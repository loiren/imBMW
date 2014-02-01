using System;
using Microsoft.SPOT;
using imBMW.Tools;

namespace imBMW.iBus.Devices.Real
{
    public enum BordmonitorFields
    {
        /// <summary>
        /// Big text, 11 chars
        /// </summary>
        Title,
        /// <summary>
        /// Small text, 11 chars
        /// </summary>
        Status,
        /// <summary>
        /// Top right, 3 chars
        /// </summary>
        Program,
        /// <summary>
        /// One of 10 items, 23 chars
        /// </summary>
        Item,
        /// <summary>
        /// One of 5 lines, ?? items
        /// </summary>
        Line
    }

    public static class Bordmonitor
    {
        public static void ShowText(string s, BordmonitorFields field, int number = 0)
        {
            ShowText(s, TextAlign.Left, field, number);
        }

        public static void ShowText(string s, TextAlign align, BordmonitorFields field, int number = 0)
        {
            int len;
            byte[] data;
            switch (field)
            {
                case BordmonitorFields.Title:
                    len = 11;
                    data = new byte[] { 0x23, 0x62, 0x10 };
                    break;
                case BordmonitorFields.Status:
                    len = 11;
                    data = new byte[] { 0xA5, 0x62, 0x01, 0x06 };
                    break;
                default:
                    throw new Exception("TODO");
            }
            var offset = data.Length;
            data = data.PadRight(0x19, len);
            data.PasteASCII(s.UTF8ToASCII(), offset, len);
            Manager.EnqueueMessage(new Message(iBus.DeviceAddress.Radio, iBus.DeviceAddress.GraphicsNavigationDriver, "Show message on BM: " + s, data));
        }
    }
}