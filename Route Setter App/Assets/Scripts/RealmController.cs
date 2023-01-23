using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Realms.Exceptions;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RealmController : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_Text userExistsText;

    static public RealmController Instance;

    private Realm _realm;
    private App _realmApp;
    private User _realmUser;
    private string _username = "";

    [SerializeField] private string _realmAppId = "pumpedinprogress-jnbvc";

    async void Awake()
    {
        // connection logic
        DontDestroyOnLoad(gameObject);
        Instance = this;

        if (_realm == null)
        {
            _realmApp = App.Create(new AppConfiguration(_realmAppId));
            if (_realmApp.CurrentUser == null)
            {
                SceneManager.LoadScene(3); // this may not work

                while (_username == "")
                {
                    GetUsername();
                }

                _realmUser = await _realmApp.LogInAsync(Credentials.Anonymous()); //LoginEmailPassword(credentials);
                
                _realm = await Realm.GetInstanceAsync(new PartitionSyncConfiguration(_realmUser.Id, _realmUser));
            }
            else
            {
                _realmUser = _realmApp.CurrentUser;
                _realm = Realm.GetInstance(new PartitionSyncConfiguration(_realmUser.Id, _realmUser));
            }
        }
    }

    private void OnDisable()
    {
        if (_realm != null)
        {
            _realm.Dispose();
        }
    }

    public bool isRealmReady()
    {
        return _realm != null;
    }

    public void GetUsername()
    {
        var username = usernameInput.text;
        var users = _realm.All<PlayerProfile>().Where(player => player.Username == username);

        if (!users.Any())
        {
            userExistsText.text = "";
            _username = username;
            CreateOrRenamePlayerProfile(_username);

            // SceneManager.UnloadSceneAsync(3);
        }
        else
        {
            userExistsText.text = "user already exists, try again";
        }
    }

    private void CreateOrRenamePlayerProfile(string username)
    {
        PlayerProfile playerProfile = _realm.All<PlayerProfile>().Where(player => player.UserId == _realmUser.Id).FirstOrDefault();
        
        if (playerProfile == null) 
        {
            _realm.Write(() =>
            {
                _realm.Add(new PlayerProfile(_realmUser.Id, username));
            });
        }

        _realm.Write(() =>
        {
            playerProfile.Username = username;
        });
    }

    private PlayerProfile GetOrCreatePlayerProfile()
    {
        // get user
        PlayerProfile playerProfile = _realm.All<PlayerProfile>().Where(player => player.UserId == _realmUser.Id).FirstOrDefault();
        
        if (playerProfile == null) // change
        {
            _realm.Write(() =>
            {
                playerProfile = _realm.Add(new PlayerProfile(_realmUser.Id));
            });
        }

        return playerProfile;
    }

    public IList<PlayerProfile_user_climbs> GetClimbData()
    {
        PlayerProfile playerProfile = GetOrCreatePlayerProfile();
        return playerProfile.UserClimbs;
    }

    public List<PlayerProfile> GetFriendsData()
    {
        PlayerProfile playerProfile = GetOrCreatePlayerProfile();
        var usernames = playerProfile.Friends.ToList();
        usernames.Add(playerProfile.Username);

        var players = _realm.All<PlayerProfile>();
        List<PlayerProfile> playerAndFriendsProfiles = new List<PlayerProfile>();

        foreach (var player in players)
        {
            if (usernames.Contains(player.Username))
            {
                playerAndFriendsProfiles.Add(player);
            }
        }

        return playerAndFriendsProfiles;
    }

    public PlayerProfile_user_climbs GetNewestClimbData()
    {
        PlayerProfile playerProfile = GetOrCreatePlayerProfile();
        return playerProfile.UserClimbs.Last();
    }

    public void AddClimb(PlayerProfile_user_climbs climb)
    {
        PlayerProfile playerProfile = GetOrCreatePlayerProfile();
        _realm.Write(() =>
        {
            playerProfile.UserClimbs.Add(climb);
        });
    }
}
