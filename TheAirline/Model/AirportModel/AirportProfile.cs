﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheAirline.Model.GeneralModel;

namespace TheAirline.Model.AirportModel
{
    //the class for a profile for an airport
    public class AirportProfile
    {
        public enum AirportType { Long_Haul_International, Regional, Domestic,Short_Haul_International }
        public AirportType Type { get; set; }
        public enum AirportSize { Smallest, Very_small, Small, Medium, Large, Very_large, Largest }
        public string Name { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Town { get; set; }
        public Country Country { get; set; }
        public Coordinates Coordinates { get; set; }
        public AirportSize Size { get; set; }
        public string Logo { get; set; }
        // chs, 2012-23-01 added for airport maps
        public string Map { get; set; }
        public TimeSpan OffsetGMT { get; set; }
        public TimeSpan OffsetDST { get; set; }
        public GameTimeZone TimeZone { get { return getTimeZone();} set { ;} }
      
        public AirportProfile(string name, string code,string icaocode, AirportType type,string town, Country country, TimeSpan offsetGMT, TimeSpan offsetDST, Coordinates coordinates, AirportSize size)
        {
            this.Name = name;
            this.IATACode = code;
            this.ICAOCode = icaocode;
            this.Type = type;
            this.Town = town;
            this.Country = country;
            this.Coordinates = coordinates;
            this.Size = size;
            this.Logo = "";
            this.OffsetDST = offsetDST;
            this.OffsetGMT = offsetGMT;
            
        }
        //returns the time zone for the airport
        private GameTimeZone getTimeZone()
        {
            GameTimeZone zone = TimeZones.GetTimeZones().Find(delegate(GameTimeZone gtz) { return gtz.UTCOffset == this.OffsetDST; });
          
            return zone;
        }
    }
}
