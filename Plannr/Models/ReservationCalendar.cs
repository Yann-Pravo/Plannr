using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
	public class ReservationCalendar
	{

        public int id;
        public int start;
        public int end;
        public String title;

        public static String ReservationsToJson(List<Reservation> liste)
        {
            List<ReservationCalendar> resaJSON = new List<ReservationCalendar>();

            liste.ForEach(x => resaJSON.Add(x.ConvertObject()));

            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return JsonConvert.SerializeObject(resaJSON, Formatting.None, jsSettings);

        }

	}
}