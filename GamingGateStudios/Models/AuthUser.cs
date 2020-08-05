using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingGateStudios.Models
{
    public class AuthUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Member_ID { get; set; }
        public string Color { get; set; }
        public string Face_ID { get; set; }
        public AuthUser(int id,string name,int member_id,string color, string face_id=null)
        {
            this.ID = id;
            this.Name = name;
            this.Member_ID = member_id;
            this.Color = color;
            this.Face_ID = face_id;
        }
    }
}