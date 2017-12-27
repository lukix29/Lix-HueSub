using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace LixHueSub
{
    public enum EventType : int
    {
        none = -1,
        sub = 0,
        resub = 1,
        raid = 2,
        ritual = 3,
        subgift = 4,
        Host = 5,
        Donation = 6,
        Bits = 7,
        allsubs = 8
    }

    public interface IHueEvent
    {
        bool Equals(EventType eventType);
        int Amount { get; set; }
        EventType EventType { get; set; }
        SubType SubType { get; set; }
    }

    public class HueEvent
    {
        public HueEvent()
        {
        }

        public HueEvent(int Key)
        {
            States.Add(new HueEventStatus() { LightKey = Key });
        }

        [JsonRequired, JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter)),
            DescriptionAttribute(@"Type of Event to trigger.
(Tiered Subs are not yet implemented!)"), DefaultValue(EventType.allsubs)]
        public EventType @EventType { get; set; } = EventType.allsubs;

        [JsonRequired, DescriptionAttribute(@"Amount e.g. Sub-Months, Host-Amount... .
First Value = Minimum, Second Value = Maximum.
If Minimum and Maximum are 0, Amount is ignored.")]
        public Point Amount { get; set; } = new Point();

        [JsonRequired,
        DescriptionAttribute(@"Color:   The Color of the Lamp.
Duration:   The duration in milliseconds of the status. (250-10000ms)
LightKeys:  Keys, witch Light(s) to toggle.
Brightness: The Brightness of the Lamp. (0-255)
Timer:      Intervall of Timer, Zero to Disable.    (Switches between Color and Off)")]
        public List<HueEventStatus> States { get; set; } = new List<HueEventStatus>();

        public bool Check(IHueEvent message)
        {
            if (message.Equals(EventType))
            {
                if (Amount.Y > 0)
                {
                    if (message.Amount >= Amount.X && message.Amount <= Amount.Y)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    public class HueEventStatus
    {
        [JsonIgnore]
        private uint dur = 1000;
        [JsonIgnore]
        private uint tim = 0;
        [JsonRequired]
        private int lightKey = 1;

        [JsonRequired, DescriptionAttribute("The Brightness of the Lamp.")]
        public byte Brightness { get; set; } = byte.MaxValue;

        [JsonRequired, DescriptionAttribute("The Color of the Lamp.")]
        public Color Color { get; set; } = Color.Gainsboro;

        [JsonRequired, DescriptionAttribute("Transition Time.")]
        public ushort Transition { get; set; } = 1;

        [JsonRequired, DescriptionAttribute("Turn Lamp On or Off.")]
        public bool TurnOn { get; set; } = true;

        [JsonRequired, DescriptionAttribute("Timer Intervall, Zero to Disable Timer.")]
        public uint Timer { get => tim; set => tim = LXMath.Constrain(value, 100, 10_000); }

        [JsonRequired, DescriptionAttribute("Duration of the \"Color\".")]
        public uint Duration
        {
            get => dur;
            set => dur = LXMath.Constrain(value, 250, 60_000);
        }

        [JsonIgnore, DefaultValueAttribute(1)]
        public int LightKey
        {
            get { return lightKey; }
            set
            {
                if (!HueEventHandler.Lights.Any(t0 => t0.ID == value))
                    throw new ArgumentOutOfRangeException("LighKey must Exist!\r\nAvailable Keys are: " +
                        String.Join(",", HueEventHandler.Lights.Select(p => p.ID.ToString())));

                lightKey = value;
            }
        }
    }
}