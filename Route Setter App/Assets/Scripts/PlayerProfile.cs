using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

public class PlayerProfile : RealmObject
{
    [MapTo("_id")]
    [PrimaryKey]
    public ObjectId Id { get; set; }

    [MapTo("user_climbs")]
    public IList<PlayerProfile_user_climbs> UserClimbs { get; }

    [MapTo("user_id")]
    [Required]
    public string UserId { get; set; }

    [MapTo("username")]
    [Required]
    public string Username { get; set; }

    [MapTo("friends")]
    [Required]
    public IList<string> Friends { get; }

    public PlayerProfile() { }

    public PlayerProfile(string userId, string username = "uknown_user")
    {
        UserId = userId;
        UserClimbs = null;
        Username = username;
    }
}

