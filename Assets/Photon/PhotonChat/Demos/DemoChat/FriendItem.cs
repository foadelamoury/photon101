﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH"/>
// <summary>Demo code for Photon Chat in Unity.</summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.UI;


namespace Photon.Chat.Demo
{
    /// <summary>
    /// Friend UI item used to represent the friend status as well as message. 
    /// It aims at showing how to share health for a friend that plays on a different room than you for example.
    /// But of course the message can be anything and a lot more complex.
    /// </summary>
    public class FriendItem : MonoBehaviour
    {
        [HideInInspector]
        public string FriendId
        { set => NameLabel.text = value; get => NameLabel.text;
        }

        public Text NameLabel;
        public Text StatusLabel;
        public Text Health;

        public void Awake()
        {
            Health.text = string.Empty;
        }

        public void OnFriendStatusUpdate(int status, bool gotMessage, object message)
        {
            string _status = status switch
            {
                1 => "Invisible",
                2 => "Online",
                3 => "Away",
                4 => "Do not disturb",
                5 => "Looking For Game/Group",
                6 => "Playing",
                _ => "Offline",
            };
            StatusLabel.text = _status;

            if (gotMessage)
            {
                string _health = string.Empty;
                if (message != null)
                {
                    if (message is string[] _messages && _messages.Length >= 2)
                    {
                        _health = _messages[1] + "%";
                    }
                }

                Health.text = _health;
            }
        }
    }
}