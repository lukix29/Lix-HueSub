namespace LixHueSub
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_testEvent = new System.Windows.Forms.Button();
            this.listBox_Lights = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_ConectHue = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rTB_Info = new System.Windows.Forms.RichTextBox();
            this.btn_ChatLogin = new System.Windows.Forms.Button();
            this.listBox_Chat = new System.Windows.Forms.ListBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBox_Events = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.txtB_ip = new System.Windows.Forms.TextBox();
            this.btn_conectTipee = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtB_tipeeToken = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_msg_Count = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_SaveEvents = new System.Windows.Forms.Button();
            this.btn_CreateEvent = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_testEvent
            // 
            this.btn_testEvent.AutoSize = true;
            this.btn_testEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_testEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_testEvent.Location = new System.Drawing.Point(2, 374);
            this.btn_testEvent.Margin = new System.Windows.Forms.Padding(2);
            this.btn_testEvent.Name = "btn_testEvent";
            this.btn_testEvent.Size = new System.Drawing.Size(47, 27);
            this.btn_testEvent.TabIndex = 1;
            this.btn_testEvent.Text = "Test";
            this.btn_testEvent.UseVisualStyleBackColor = true;
            this.btn_testEvent.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox_Lights
            // 
            this.listBox_Lights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Lights.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.listBox_Lights.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Lights.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox_Lights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Lights.ForeColor = System.Drawing.Color.Gainsboro;
            this.listBox_Lights.FormattingEnabled = true;
            this.listBox_Lights.IntegralHeight = false;
            this.listBox_Lights.Location = new System.Drawing.Point(401, 27);
            this.listBox_Lights.Name = "listBox_Lights";
            this.listBox_Lights.Size = new System.Drawing.Size(190, 139);
            this.listBox_Lights.TabIndex = 2;
            this.listBox_Lights.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox_Lights.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listBox1_MeasureItem);
            this.listBox_Lights.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.AutoSize = true;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(-3, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 25);
            this.button3.TabIndex = 4;
            this.button3.Text = "Reset Credentials";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_ConectHue
            // 
            this.btn_ConectHue.AutoSize = true;
            this.btn_ConectHue.Enabled = false;
            this.btn_ConectHue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConectHue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_ConectHue.Location = new System.Drawing.Point(0, 44);
            this.btn_ConectHue.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ConectHue.Name = "btn_ConectHue";
            this.btn_ConectHue.Size = new System.Drawing.Size(122, 32);
            this.btn_ConectHue.TabIndex = 5;
            this.btn_ConectHue.Text = "Connect Hue";
            this.btn_ConectHue.UseVisualStyleBackColor = true;
            this.btn_ConectHue.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rTB_Info
            // 
            this.rTB_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.rTB_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rTB_Info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTB_Info.ForeColor = System.Drawing.Color.Gainsboro;
            this.rTB_Info.Location = new System.Drawing.Point(0, 138);
            this.rTB_Info.Name = "rTB_Info";
            this.rTB_Info.ReadOnly = true;
            this.rTB_Info.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rTB_Info.ShortcutsEnabled = false;
            this.rTB_Info.Size = new System.Drawing.Size(278, 69);
            this.rTB_Info.TabIndex = 7;
            this.rTB_Info.Text = "";
            // 
            // btn_ChatLogin
            // 
            this.btn_ChatLogin.AutoSize = true;
            this.btn_ChatLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ChatLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ChatLogin.Location = new System.Drawing.Point(0, 96);
            this.btn_ChatLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ChatLogin.Name = "btn_ChatLogin";
            this.btn_ChatLogin.Size = new System.Drawing.Size(148, 32);
            this.btn_ChatLogin.TabIndex = 2;
            this.btn_ChatLogin.Text = "Chat Login";
            this.btn_ChatLogin.UseVisualStyleBackColor = true;
            this.btn_ChatLogin.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox_Chat
            // 
            this.listBox_Chat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_Chat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.listBox_Chat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Chat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox_Chat.Enabled = false;
            this.listBox_Chat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Chat.ForeColor = System.Drawing.Color.Gainsboro;
            this.listBox_Chat.FormattingEnabled = true;
            this.listBox_Chat.Location = new System.Drawing.Point(5, 27);
            this.listBox_Chat.Name = "listBox_Chat";
            this.listBox_Chat.Size = new System.Drawing.Size(390, 171);
            this.listBox_Chat.TabIndex = 8;
            this.listBox_Chat.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox2_DrawItem);
            this.listBox_Chat.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listBox2_MeasureItem);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsBorderColor = System.Drawing.Color.DimGray;
            this.propertyGrid1.CommandsDisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.propertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.propertyGrid1.Enabled = false;
            this.propertyGrid1.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.propertyGrid1.HelpForeColor = System.Drawing.Color.Gainsboro;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedItemWithFocusBackColor = System.Drawing.SystemColors.HotTrack;
            this.propertyGrid1.SelectedItemWithFocusForeColor = System.Drawing.Color.Gainsboro;
            this.propertyGrid1.Size = new System.Drawing.Size(301, 187);
            this.propertyGrid1.TabIndex = 10;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.propertyGrid1.ViewForeColor = System.Drawing.Color.Gainsboro;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            this.propertyGrid1.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGrid1_SelectedGridItemChanged);
            this.propertyGrid1.Click += new System.EventHandler(this.propertyGrid1_Click);
            // 
            // listBox_Events
            // 
            this.listBox_Events.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Events.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(49)))), ((int)(((byte)(49)))));
            this.listBox_Events.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_Events.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBox_Events.ForeColor = System.Drawing.Color.Gainsboro;
            this.listBox_Events.FormattingEnabled = true;
            this.listBox_Events.IntegralHeight = false;
            this.listBox_Events.Location = new System.Drawing.Point(0, 19);
            this.listBox_Events.Name = "listBox_Events";
            this.listBox_Events.Size = new System.Drawing.Size(242, 350);
            this.listBox_Events.TabIndex = 11;
            this.listBox_Events.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_Events_DrawItem);
            this.listBox_Events.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listBox_Events_MeasureItem);
            this.listBox_Events.SelectedIndexChanged += new System.EventHandler(this.listBox_Events_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.rTB_Info);
            this.panel1.Location = new System.Drawing.Point(5, 201);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(279, 208);
            this.panel1.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.btn_ConectHue);
            this.splitContainer1.Panel1.Controls.Add(this.txtB_ip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_conectTipee);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.btn_ChatLogin);
            this.splitContainer1.Panel2.Controls.Add(this.txtB_tipeeToken);
            this.splitContainer1.Size = new System.Drawing.Size(278, 130);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Hue IP:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtB_ip
            // 
            this.txtB_ip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtB_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_ip.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtB_ip.Location = new System.Drawing.Point(0, 19);
            this.txtB_ip.Name = "txtB_ip";
            this.txtB_ip.Size = new System.Drawing.Size(122, 22);
            this.txtB_ip.TabIndex = 22;
            this.txtB_ip.TextChanged += new System.EventHandler(this.txtB_ip_TextChanged);
            // 
            // btn_conectTipee
            // 
            this.btn_conectTipee.AutoSize = true;
            this.btn_conectTipee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_conectTipee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_conectTipee.Location = new System.Drawing.Point(0, 44);
            this.btn_conectTipee.Margin = new System.Windows.Forms.Padding(0);
            this.btn_conectTipee.Name = "btn_conectTipee";
            this.btn_conectTipee.Size = new System.Drawing.Size(148, 32);
            this.btn_conectTipee.TabIndex = 26;
            this.btn_conectTipee.Text = "Connect Tipeee";
            this.btn_conectTipee.UseVisualStyleBackColor = true;
            this.btn_conectTipee.Click += new System.EventHandler(this.btn_conectTipee_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(-1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 16);
            this.label7.TabIndex = 25;
            this.label7.Text = "Tipeee APi Token";
            // 
            // txtB_tipeeToken
            // 
            this.txtB_tipeeToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtB_tipeeToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.txtB_tipeeToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtB_tipeeToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtB_tipeeToken.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtB_tipeeToken.Location = new System.Drawing.Point(0, 19);
            this.txtB_tipeeToken.Name = "txtB_tipeeToken";
            this.txtB_tipeeToken.Size = new System.Drawing.Size(149, 22);
            this.txtB_tipeeToken.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(126, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 25);
            this.button1.TabIndex = 21;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lbl_msg_Count
            // 
            this.lbl_msg_Count.AutoSize = true;
            this.lbl_msg_Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg_Count.Location = new System.Drawing.Point(281, 6);
            this.lbl_msg_Count.Name = "lbl_msg_Count";
            this.lbl_msg_Count.Size = new System.Drawing.Size(15, 16);
            this.lbl_msg_Count.TabIndex = 20;
            this.lbl_msg_Count.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(176, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Chat Messages: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Incomming Events";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(401, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Available Lamps";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Edit Event";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(-3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Hue Light Events";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btn_delete);
            this.panel2.Controls.Add(this.btn_SaveEvents);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btn_testEvent);
            this.panel2.Controls.Add(this.listBox_Events);
            this.panel2.Location = new System.Drawing.Point(597, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 403);
            this.panel2.TabIndex = 17;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btn_delete
            // 
            this.btn_delete.AutoSize = true;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(78, 374);
            this.btn_delete.Margin = new System.Windows.Forms.Padding(2);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(55, 27);
            this.btn_delete.TabIndex = 18;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_SaveEvents
            // 
            this.btn_SaveEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveEvents.AutoSize = true;
            this.btn_SaveEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveEvents.Location = new System.Drawing.Point(158, 374);
            this.btn_SaveEvents.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SaveEvents.Name = "btn_SaveEvents";
            this.btn_SaveEvents.Size = new System.Drawing.Size(85, 27);
            this.btn_SaveEvents.TabIndex = 17;
            this.btn_SaveEvents.Text = "Save Events";
            this.btn_SaveEvents.UseVisualStyleBackColor = true;
            this.btn_SaveEvents.Click += new System.EventHandler(this.btn_SaveEvents_Click);
            // 
            // btn_CreateEvent
            // 
            this.btn_CreateEvent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_CreateEvent.AutoSize = true;
            this.btn_CreateEvent.Enabled = false;
            this.btn_CreateEvent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CreateEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CreateEvent.Location = new System.Drawing.Point(401, 171);
            this.btn_CreateEvent.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CreateEvent.Name = "btn_CreateEvent";
            this.btn_CreateEvent.Size = new System.Drawing.Size(114, 27);
            this.btn_CreateEvent.TabIndex = 8;
            this.btn_CreateEvent.Text = "Create new Event\r\n";
            this.btn_CreateEvent.UseVisualStyleBackColor = true;
            this.btn_CreateEvent.Click += new System.EventHandler(this.btn_CreateEvent_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.propertyGrid1);
            this.panel3.Location = new System.Drawing.Point(290, 201);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 208);
            this.panel3.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSize = true;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(-4, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 25);
            this.button2.TabIndex = 24;
            this.button2.Text = "Remove App from Hue";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(843, 413);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btn_CreateEvent);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_msg_Count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox_Lights);
            this.Controls.Add(this.listBox_Chat);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Name = "Form1";
            this.Text = "Lix Hue Sub";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_testEvent;
        private System.Windows.Forms.ListBox listBox_Lights;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_ConectHue;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox rTB_Info;
        private System.Windows.Forms.Button btn_ChatLogin;
        private System.Windows.Forms.ListBox listBox_Chat;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListBox listBox_Events;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_CreateEvent;
        private System.Windows.Forms.Button btn_SaveEvents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_msg_Count;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtB_ip;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtB_tipeeToken;
        private System.Windows.Forms.Button btn_conectTipee;
        private System.Windows.Forms.Button button2;
    }
}

