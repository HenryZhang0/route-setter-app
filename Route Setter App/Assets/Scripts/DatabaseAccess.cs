using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://routesetting:routesetting@routesettercluster.tg7w2dt.mongodb.net/?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("ClimbingDatabase");
        collection = database.GetCollection<BsonDocument>("ClimbingCollection");
    }

    public async void SaveClimbToDataBase(ClimbData climb)
    {
        var document = new BsonDocument {
            { "climbName", climb.climbName },
            { "vGrade", climb.vGrade },
            { "dateCompleted", climb.dateCompleted }
        };

        await collection.InsertOneAsync(document);
    }

    public async Task<List<ClimbData>> GetClimbsFromDataBase()
    {
        var allClimbsTask = collection.FindAsync(new BsonDocument()); //check mongodb if u wanna get other specific documents
        var climbsAwaited = await allClimbsTask;

        List<ClimbData> climbs = new List<ClimbData>();
        foreach (var climb in climbsAwaited.ToList())
        {
            climbs.Add(Deserialize(climb));
        }
        return climbs;
    }

    private ClimbData Deserialize(BsonDocument bsonClimb)
    {
        // example { "_id" : ObjectId("6382e3499752c795f8daa498"), "playerName" : "Joyce", "climbName" : "double clutch", "vGrade" : "V3", "dateCompleted" : "Dec 3, 2020" }
        var climb = new ClimbData();
        // prob easier way to deserialize bson... couldnt find solution
        climb.climbName = bsonClimb.GetValue("climbName").AsString;
        climb.vGrade = bsonClimb.GetValue("vGrade").AsString;
        climb.dateCompleted = bsonClimb.GetValue("dateCompleted").AsString;
        return climb;
    }
}
