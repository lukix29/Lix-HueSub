using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace LixHueSub
{
    public partial class Form1 : Form
    {
        private HueEventHandler hueEventHandler = new HueEventHandler();

        private int RefreshIntervalCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public SubMessage SelectedSub
        {
            get { return (SubMessage)listBox_Chat.SelectedItem; }
        }

        private void btn_CreateEvent_Click(object sender, EventArgs e)
        {
            var item = (SharpHue.Light)listBox_Lights.SelectedItem;
            var hueevent = new HueEvent(item.ID);
            hueEventHandler.Events.Add(hueevent);
            listBox_Events.Items.Add(hueevent);
            listBox_Events.Refresh();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_Events.SelectedIndex >= 0)
                {
                    listBox_Events.Items.RemoveAt(listBox_Events.SelectedIndex);
                    hueEventHandler?.Events.RemoveAt(listBox_Events.SelectedIndex);
                }
            }
            catch
            {
            }
        }

        private void btn_SaveEvents_Click(object sender, EventArgs e)
        {
            hueEventHandler?.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox_Events.SelectedIndex >= 0)
                {
                    var item = (HueEvent)listBox_Events.SelectedItem;
                    System.Threading.Tasks.Task.Run(() => hueEventHandler?.FireEvent(item));
                }
            }
            catch (Exception x)
            {
                x.Handle("", true);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox_Chat.Items.Clear();
            hueEventHandler.IRC.SubList.Clear();
            if (System.IO.File.Exists("subcache.json"))
            {
                System.IO.File.Delete("subcache.json");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LX29_MessageBox.Show("Clear Hue Key and Twitch Chat Login?", "Clear?", MessageBoxButtons.YesNo) == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                //Application.Restart();
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await InitailizeHueConection(txtB_ip.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                btn_ChatLogin.Visible = false;
                hueEventHandler.OnSubReceived += Irc_OnSubReceived;
                hueEventHandler.LoginChat(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        listBox_Chat.Enabled = true;
                        btn_ChatLogin.Enabled = false;
                        hueEventHandler.IRC.OnError = (error) =>
                        {
                            try
                            {
                                this.Invoke(new Action(() =>
                                {
                                    rTB_Info.AppendText(error.Message + "\r\n");
                                    rTB_Info.Select(rTB_Info.TextLength, 1);
                                    rTB_Info.ScrollToCaret();
                                }));
                            }
                            catch { }
                        };
                    }));
                });
            }
            catch (Exception x) { x.Handle("", true); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SharpHue.Light.OnStateSet -= HueEventHandler_OnEventFired;
                hueEventHandler?.Save();
                hueEventHandler?.Dispose();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
#if !DEBUG
            btn_conectTipee.Visible = false;
            txtB_tipeeToken.Visible = false;
            label7.Visible = false;
#endif
                //propertyGrid1.SelectedObject = hueEvent;
                //InitailizeHueConection();
                hueEventHandler.LoadEvents(listBox_Chat, listBox_Events);

                SharpHue.Light.OnStateSet += HueEventHandler_OnEventFired;

                txtB_ip.Text = Properties.Settings.Default.LastIP;

                txtB_tipeeToken.Text = Properties.Settings.Default.TipeeeToken;
            }
            catch (Exception x)
            {
                x.Handle("", true);
                Application.Exit();
            }
        }

        private void HueEventHandler_OnEventFired(Newtonsoft.Json.Linq.JObject status)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    rTB_Info.AppendText(status.ToString(Newtonsoft.Json.Formatting.None)
                        .ReplaceAll("", "{", "}", "\"") + "\r\n");
                    rTB_Info.Select(rTB_Info.TextLength, 1);
                    rTB_Info.ScrollToCaret();
                }));
            }
            catch { }
        }

        //private void Hb_PushButtonOnBridge()
        //{
        //    rTB_Info.Text = "Please press the button on the bridge to register the application in the next minute.";
        //}

        private const int retryCount = 60;
        private async Task<bool> InitailizeHueConection(string ip, int cnt = 0)
        {
            try
            {
                //HueBridgeLocator.OnUpdatedInfo += (info) =>
                //{
                //    this.Invoke(new Action(() => { rTB_Info.Text = "Searching: " + info; }));
                //};
                if (cnt > 0)
                {

                    rTB_Info.Text = "Press the Button on the Bridge. (" + (retryCount - cnt) + "s)";
                }
                else
                {
                    rTB_Info.Text = "Connecting to Hue.";
                }

                listBox_Lights.Enabled = false;
                timer1.Enabled = false;

                var error = await hueEventHandler.FindBridge(ip);
                if (error == null)// HueBridgeLocator.Locate();
                {
                    rTB_Info.Clear();
                    rTB_Info.AppendText("Found Hue, registering.\r\n");

                    Properties.Settings.Default.LastIP = SharpHue.Configuration.DeviceIP.ToString();
                    Properties.Settings.Default.Save();

                    listBox_Lights.Enabled = true;
                    timer1.Enabled = true;
                    btn_testEvent.Enabled = true;

                    listBox_Lights.Items.Clear();
                    foreach (var light in HueEventHandler.Lights)
                    {
                        listBox_Lights.Items.Add(light);
                    }

                    rTB_Info.AppendText("Registered Hue.\r\n");

                    return true;
                }
                else if (cnt == 0)
                {
                    if (error is SharpHue.HueApiException)
                    {
                        var hae = (SharpHue.HueApiException)error;
                        if (hae.ErrorID == 101)
                        {
                            for (int i = 1; i <= retryCount; i += 2)
                            {
                                if (await InitailizeHueConection(ip, i))
                                {
                                    return true;
                                }
                                else
                                {
                                    await Task.Delay(1000);
                                }
                            }
                        }
                    }
                    rTB_Info.AppendText(error.Message);
                    listBox_Lights.Enabled = false;
                    timer1.Enabled = false;
                }
                rTB_Info.Select(rTB_Info.TextLength, 1);
                rTB_Info.ScrollToCaret();
                return false;
            }
            catch (Exception x)
            {
                x.Handle("", true);
                return false;
            }
        }

        private void Irc_OnSubReceived(SubMessage message)
        {
            try
            {
                this.Invoke(new Action(() => listBox_Chat.Items.Insert(0, message)));
            }
            catch { }
        }

        private void listBox_Events_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //make schöner
                e.DrawBackground();
                e.DrawFocusRectangle();
                var text = listBox_Events.Items[e.Index].ToString();
                text = text.RemoveFrom("\"Statuses").Trim("\r\n", ",");
                TextRenderer.DrawText(e.Graphics, text,
                    e.Font, e.Bounds.Location, e.ForeColor, TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                TextRenderer.DrawText(e.Graphics, "#" + e.Index + " ", btn_ConectHue.Font, e.Bounds, Color.Firebrick, e.BackColor,
                    TextFormatFlags.Right | TextFormatFlags.Top | TextFormatFlags.NoClipping);
                e.Graphics.DrawRectangle(Pens.Firebrick, e.Bounds);
            }
            catch { }
        }

        private void listBox_Events_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                var text = listBox_Events.Items[e.Index].ToString();
                text = text.RemoveFrom("\"Statuses").Trim("\r\n", ",");
                var size = TextRenderer.MeasureText(e.Graphics, text, listBox_Events.Font);
                e.ItemHeight = size.Height + 2;
                e.ItemWidth = size.Width;
            }
            catch { }
        }

        private void listBox_Events_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = (HueEvent)listBox_Events.SelectedItem;
            propertyGrid1.Enabled = true;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //make schöner
                e.DrawBackground();
                e.DrawFocusRectangle();
                TextRenderer.DrawText(e.Graphics, listBox_Lights.Items[e.Index].ToString(),
                    e.Font, e.Bounds.Location, e.ForeColor, TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                e.Graphics.DrawRectangle(Pens.Firebrick, e.Bounds);
            }
            catch { }
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                var size = TextRenderer.MeasureText(e.Graphics, listBox_Lights.Items[e.Index].ToString(), listBox_Lights.Font);
                e.ItemHeight = size.Height + 3;
                e.ItemWidth = size.Width;
            }
            catch { }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Lights.SelectedIndex >= 0)
            {
                // var items = listBox_Lights.SelectedItem.Cast<SharpHue.Light>().ToArray();
                btn_CreateEvent.Enabled = true;
            }
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //make schöner
                e.DrawBackground();
                e.DrawFocusRectangle();
                TextRenderer.DrawText(e.Graphics, listBox_Chat.Items[e.Index].ToString(),
                    e.Font, e.Bounds.Location, e.ForeColor, TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                e.Graphics.DrawRectangle(Pens.Firebrick, e.Bounds);
            }
            catch { }
        }

        private void listBox2_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                var size = TextRenderer.MeasureText(e.Graphics, listBox_Chat.Items[e.Index].ToString(), listBox_Chat.Font);
                e.ItemHeight = size.Height;
                e.ItemWidth = size.Width;
            }
            catch { }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //listBox_Events.Items.Clear();
            ////listBox_Events.BeginUpdate();
            //foreach (var eve in hueEventHandler.Events)
            //{
            //    listBox_Events.Items.Add(eve);
            //}
            ////listBox_Events.EndUpdate();
            listBox_Events.Refresh();
        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //HueEventHandler.Lights.Refresh();

                //listBox_Lights.BeginUpdate();
                //var idx = listBox_Lights.SelectedIndex;
                //listBox_Lights.Items.Clear();
                //foreach (var light in HueEventHandler.Lights)
                //{
                //    listBox_Lights.Items.Add(light);
                //}
                //listBox_Lights.SelectedIndex = idx;
                //listBox_Lights.EndUpdate();
            }
            catch { }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (RefreshIntervalCount++ > 5)
                {
                    if (hueEventHandler.IsChatConected)
                    {
                        listBox_Chat.Refresh();
                    }
                    listBox_Events.Refresh();
                    listBox_Lights.Refresh();

                    RefreshIntervalCount = 0;
                }
                lbl_msg_Count.Text = hueEventHandler.IRC.MessageCount.SizeMag(System.Globalization.CultureInfo.CurrentCulture, "G29");
            }
            catch { }
        }

        private void txtB_ip_TextChanged(object sender, EventArgs e)
        {
            btn_ConectHue.Enabled = System.Net.IPAddress.TryParse(txtB_ip.Text, out System.Net.IPAddress adress);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_conectTipee_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TipeeeToken = txtB_tipeeToken.Text;
            Properties.Settings.Default.Save();

            hueEventHandler?.Tipeee.LoginTipeee(txtB_tipeeToken.Text, () =>
             {
                 this.Invoke(new Action(() => { txtB_tipeeToken.Enabled = btn_conectTipee.Enabled = false; }));
             });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var config = SharpHue.Configuration.GetBridgeConfiguration();
            foreach (var user in config.Whitelist)
            {
                if (SharpHue.Configuration.DeleteUser(user.Key))
                {

                }
            }
        }
    }
}