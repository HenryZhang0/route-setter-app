using System;
using System.Collections.Generic;
using Realms;

public class PlayerProfile_user_climbs : EmbeddedObject // this used to be called climbdata but mongodb schema naming isnt nice. will change to own database later :P
{
    [MapTo("climbName")]
    public string ClimbName { get; set; }

    [MapTo("dateCompleted")]
    public string DateCompleted { get; set; }

    [MapTo("vGrade")]
    public string VGrade { get; set; }

    public PlayerProfile_user_climbs() { }

    public PlayerProfile_user_climbs(string vGrade, string climbName, string dateCompleted, RouteData routeData = null)
    {
        VGrade = vGrade;
        ClimbName = climbName;
        DateCompleted = dateCompleted; // change this to datetime type later
        //this.routeData = routeData;
    }
}