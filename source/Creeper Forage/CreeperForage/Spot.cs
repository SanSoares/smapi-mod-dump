﻿using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreeperForage
{
    public class Spot
    {
        public string NPC;
        public string Location;
        public int X;
        public int Y;
        public int PercentChance;
        public static Dictionary<string, List<string>> Loot;
        public static List<Spot> Spots;

        public static void Roll(object sender, EventArgs e)
        {
            int base_luck = (int)((StardewValley.Game1.dailyLuck + 1f) * 500f); //rng 0-100

            Random rng = new Random(DateTime.Now.Millisecond);

            foreach (Spot ss in Spots)
            {
                //despawn old items
                GameLocation l = Game1.getLocationFromName(ss.Location);
                Vector2 pos = new Vector2(ss.X, ss.Y);
                if (l.objects.ContainsKey(pos))
                {
                    //if there's one of our own items here, remove it
                    StardewValley.Object o1 = l.objects[pos];
                    if (o1 != null && o1.displayName != null)
                    {
                        if (o1.displayName.Contains("Panties") || o1.displayName.Contains("Underwear"))
                        {
                            DebugLog("Despawning item from " + ss.Location, LogLevel.Debug);
                            l.objects.Remove(pos);
                        }
                    }
                }

                //spawn a new item, if desireable
                if (!l.objects.ContainsKey(pos))
                {
                    //is npc enabled?
                    if (Config.GetNPC(ss.NPC).Enabled)
                    {
                        if (Loot.ContainsKey(ss.NPC))
                        {
                            if(Loot[ss.NPC].Count > 0)
                            {
                                int strikepoint = rng.Next(2001);
                                int chance = (int)(((((float)ss.PercentChance) / 100f) * (((float)base_luck) / 100f)) * 100f);
                                if (chance > strikepoint)
                                {
                                    Item ei = Item.items[Loot[ss.NPC].ElementAt(new Random(DateTime.Now.Millisecond).Next(Loot[ss.NPC].Count))];
                                    StardewValley.Object i = (StardewValley.Object)StardewValley.Objects.ObjectFactory.getItemFromDescription(0, ei.internal_id, 1);
                                    i.IsSpawnedObject = true; //.isSpawnedObject.Value = true;
                                    i.Quality = ss.RollQuality();// quality.Value = ss.RollQuality(); //1.3
                                    i.ParentSheetIndex = ei.internal_id;
                                    DebugLog("Spawning item " + ei.unique_id + " at " + ss.Location + " (" + ss.X + ", " + ss.Y + ")", LogLevel.Debug);
                                    l.objects.Add(pos, i);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int RollQuality()
        {
            int luv = Stardew.GetFriendshipPoints(NPC);
            Random rng = new Random(DateTime.Now.Millisecond);
            int mq = luv / 500;
            int quality = (mq > 0 && rng.Next(100) < 15 * mq) ? 1 : 0;
            if(mq > 1 && rng.Next(100) < 10 * mq){
                quality = 2;
                if(mq > 2 && rng.Next(100) < 5 * mq){
                    quality = 3;
                    if(mq > 3 && rng.Next(100) < 4) quality = 4;
                }
            }
            return quality;
        }


        public static void DebugLog(string msg, LogLevel level)
        {
            #if DEBUG
                Mod.instance.Monitor.Log(msg, level);
            #endif
        }

        public static void Setup(IModHelper helper)
        {

            helper.Events.GameLoop.DayStarted += Roll;

            Loot = new Dictionary<string, List<string>>();
            Loot["Haley"] = new List<string> { "px.haley1", "px.haley2" };
            Loot["Emily"] = new List<string> { "px.emily1", "px.emily2" };
            Loot["Penny"] = new List<string> { "px.penny1", "px.penny2" };
            Loot["Jodi"] = new List<string> { "px.jodi1", "px.jodi1" };
            Loot["Leah"] = new List<string> { "px.leah1", "px.leah2" };
            Loot["Caroline"] = new List<string> { "px.caroline1", "px.caroline2" };
            Loot["Abigail"] = new List<string> { "px.abigail1", "px.abigail2" };
            Loot["Maru"] = new List<string> { "px.maru1", "px.maru2" };
            Loot["Robin"] = new List<string> { "px.robin1", "px.robin2" };
            Loot["Alex"] = new List<string> { "px.alex1", "px.alex2" };
            Loot["Elliott"] = new List<string> { "px.elliott1", "px.elliott2" };
            Loot["Harvey"] = new List<string> { "px.harvey1", "px.harvey2" };
            Loot["Sam"] = new List<string> { "px.sam1", "px.sam2" };
            Loot["Sebastian"] = new List<string> { "px.sebastian1", "px.sebastian2" };
            Loot["Shane"] = new List<string> { "px.shane1", "px.shane2" };
            Loot["Clint"] = new List<string> { "px.clint1", "px.clint2" };
            Loot["Demetrius"] = new List<string> { "px.demetrius1", "px.demetrius2" };
            Loot["Gus"] = new List<string> { "px.gus1", "px.gus2" };
            Loot["Kent"] = new List<string> { "px.kent1", "px.kent2" };
            Loot["Lewis"] = new List<string> { "px.lewis1", "px.lewis2" };
            Loot["Marnie"] = new List<string> { "px.marnie1", "px.marnie2" };
            Loot["Pam"] = new List<string> { "px.pam1", "px.pam2" };
            Loot["Pierre"] = new List<string> { "px.pierre1", "px.pierre2" };
            Loot["Sandy"] = new List<string> { "px.sandy1", "px.sandy2" };
            Loot["Willy"] = new List<string> { "px.willy1", "px.willy2" };
            Loot["Wizard"] = new List<string> { "px.wizard1", "px.wizard2" };
            Loot["Vincent"] = new List<string> { "px.vincent1", "px.vincent2" };
            Loot["Jas"] = new List<string> { "px.jas1", "px.jas2" };
            Loot["Linus"] = new List<string> { "px.linus1", "px.linus2" };
            Loot["Evelyn"] = new List<string> { "px.1evelyn", "px.evelyn2" };
            Loot["George"] = new List<string> { "px.george1", "px.george2" };

            //order is important for bath house odds
            Spots = new List<Spot>();
            int very_rare = 1; //wrong house o.O - otherspots
            int rare = 5; //wrong part of the house - otherspots
            int normal = 20; //bedroom, usually - homespots
            string npcName;

            if (Config.GetNPC("Sandy").HomeSpots)
            {
                Spots.Add(new Spot("Sandy", "SandyHouse", 2, 4, normal)); //br
                Spots.Add(new Spot("Sandy", "SandyHouse", 18, 4, normal)); //br
            }
            if (Config.GetNPC("Sandy").OtherSpots)
            {
                Spots.Add(new Spot("Sandy", "Desert", 41, 54, rare)); //bench southeast
                Spots.Add(new Spot("Sandy", "Desert", 2, 51, very_rare)); //bench outside oasis
            }
            npcName = "Sandy";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                } else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                }
            }
            if (Config.GetNPC("Gus").HomeSpots)
            {
                Spots.Add(new Spot("Gus", "Saloon", 23, 6, normal)); //br
                Spots.Add(new Spot("Gus", "Saloon", 15, 5, normal)); //br
            }
            if (Config.GetNPC("Gus").OtherSpots)
            {
                Spots.Add(new Spot("Gus", "Saloon", 36, 8, rare)); //storage area
                Spots.Add(new Spot("Gus", "Saloon", 6, 6, very_rare)); //dining room
            }
            npcName = "Gus";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Kent").HomeSpots)
            {
                Spots.Add(new Spot("Kent", "SamHouse", 18, 4, normal)); //br
                Spots.Add(new Spot("Kent", "SamHouse", 20, 6, normal)); //br
            }
            if (Config.GetNPC("Kent").OtherSpots)
            {
                Spots.Add(new Spot("Kent", "SamHouse", 8, 13, rare)); //living room
                Spots.Add(new Spot("Kent", "Town", 3, 65, very_rare)); //in fence north of house
            }
            npcName = "Kent";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));;
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Lewis").HomeSpots)
            {
                Spots.Add(new Spot("Lewis", "ManorHouse", 19, 4, normal)); //br
                Spots.Add(new Spot("Lewis", "ManorHouse", 21, 6, normal)); //br
            }
            if (Config.GetNPC("Lewis").OtherSpots)
            {
                Spots.Add(new Spot("Lewis", "ManorHouse", 7, 5, rare)); //kitchen
                Spots.Add(new Spot("Lewis", "AnimalShop", 15, 4, very_rare)); //marnie's room
            }
            npcName = "Lewis";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Marnie").HomeSpots)
            {
                Spots.Add(new Spot("Marnie", "AnimalShop", 14, 4, normal)); //br
                Spots.Add(new Spot("Marnie", "AnimalShop", 16, 6, normal)); //br
            }
            if (Config.GetNPC("Marnie").OtherSpots)
            {
                Spots.Add(new Spot("Marnie", "AnimalShop", 16, 17, rare)); //by counter
                Spots.Add(new Spot("Marnie", "Forest", 83, 16, very_rare)); //by silo
            }
            npcName = "Marnie";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Pam").HomeSpots)
            {
                Spots.Add(new Spot("Pam", "Trailer", 16, 5, normal)); //br
                Spots.Add(new Spot("Pam", "Trailer", 13, 8, normal)); //br
            }
            if (Config.GetNPC("Pam").OtherSpots)
            {
                Spots.Add(new Spot("Pam", "BusStop", 30, 6, rare)); //saloon
                Spots.Add(new Spot("Pam", "Saloon", 39, 18, very_rare)); //
            }
            npcName = "Pam";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Pierre").HomeSpots)
            {
                Spots.Add(new Spot("Pierre", "SeedShop", 26, 7, normal)); //br
                Spots.Add(new Spot("Pierre", "SeedShop", 19, 4, normal)); //br
            }
            if (Config.GetNPC("Pierre").OtherSpots)
            {
                Spots.Add(new Spot("Pierre", "SeedShop", 7, 17, rare)); //behind counter
                Spots.Add(new Spot("Pierre", "Town", 16, 69, very_rare)); //by planter
            }
            npcName = "Pierre";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Elliott").HomeSpots)
            {
                Spots.Add(new Spot("Elliott", "ElliottHouse", 10, 7, normal)); //br
                Spots.Add(new Spot("Elliott", "ElliottHouse", 6, 4, normal)); //br
            }
            if (Config.GetNPC("Elliott").OtherSpots)
            {
                Spots.Add(new Spot("Elliott", "Beach", 47, 25, rare)); //beach
                Spots.Add(new Spot("Elliott", "Town", 31, 38, very_rare)); //picnic table
            }
            npcName = "Elliott";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Vincent").HomeSpots)
            {
                Spots.Add(new Spot("Vincent", "SamHouse", 12, 23, normal)); //br
                Spots.Add(new Spot("Vincent", "SamHouse", 10, 15, rare)); //outside br
            }
            if (Config.GetNPC("Vincent").OtherSpots)
            {
                Spots.Add(new Spot("Vincent", "Mountain", 89, 37, rare)); //by quarry bridge
                Spots.Add(new Spot("Vincent", "AnimalShop", 2, 5, very_rare)); //jas's room :o
            }
            npcName = "Vincent";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Jas").HomeSpots)
            {
                Spots.Add(new Spot("Jas", "AnimalShop", 7, 7, normal)); //br
                Spots.Add(new Spot("Jas", "AnimalShop", 5, 5, normal)); //br
            }
            if (Config.GetNPC("Jas").OtherSpots)
            {
                Spots.Add(new Spot("Jas", "AnimalShop", 31, 17, rare)); //kitchen
                Spots.Add(new Spot("Jas", "Town", 28, 11, very_rare)); //left of community center
            }
            npcName = "Jas";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Linus").HomeSpots)
            {
                Spots.Add(new Spot("Linus", "Mountain", 37, 5, normal)); //br
                Spots.Add(new Spot("Linus", "Tent", 1, 3, normal)); //br
            }
            if (Config.GetNPC("Linus").OtherSpots)
            {
                Spots.Add(new Spot("Linus", "Town", 67, 39, rare)); //riverside north
                Spots.Add(new Spot("Linus", "Town", 47, 68, very_rare)); //outside saloon
            }
            npcName = "Linus";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                }
            }
            if (Config.GetNPC("Evelyn").HomeSpots)
            {
                Spots.Add(new Spot("Evelyn", "JoshHouse", 6, 6, normal)); //br
                Spots.Add(new Spot("Evelyn", "JoshHouse", 2, 5, normal)); //br
            }
            if (Config.GetNPC("Evelyn").OtherSpots)
            {
                Spots.Add(new Spot("Evelyn", "JoshHouse", 1, 18, rare)); //
                Spots.Add(new Spot("Evelyn", "JoshHouse", 19, 23, very_rare)); //
            }
            npcName = "Evelyn";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("George").HomeSpots)
            {
                Spots.Add(new Spot("George", "JoshHouse", 1, 6, normal)); //br
                Spots.Add(new Spot("George", "JoshHouse", 6, 10, normal)); //br
            }
            if (Config.GetNPC("George").OtherSpots)
            {
                Spots.Add(new Spot("George", "JoshHouse", 15, 21, rare)); //
                Spots.Add(new Spot("George", "JoshHouse", 12, 20, very_rare)); //
            }
            npcName = "George";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Wizard").HomeSpots)
            {
                Spots.Add(new Spot("Wizard", "WizardHouse", 8, 7, normal)); //br
                Spots.Add(new Spot("Wizard", "WizardHouse", 4, 22, normal)); //br
            }
            if (Config.GetNPC("Wizard").OtherSpots)
            {
                Spots.Add(new Spot("Wizard", "WitchHut", 5, 8, rare)); //huh
                Spots.Add(new Spot("Wizard", "Forest", 4, 22, very_rare)); //outside tower
            }
            npcName = "Wizard";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                }
            }
            if (Config.GetNPC("Willy").HomeSpots)
            {
                Spots.Add(new Spot("Willy", "FishShop", 6, 4, normal)); //br
                Spots.Add(new Spot("Willy", "FishShop", 9, 6, normal)); //br
            }
            if (Config.GetNPC("Willy").OtherSpots)
            {
                Spots.Add(new Spot("Willy", "Beach", 13, 38, rare)); //pier
                Spots.Add(new Spot("Willy", "Beach", 43, 35, very_rare)); //end of docks
            }
            npcName = "Willy";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Haley").HomeSpots)
            {
                Spots.Add(new Spot("Haley", "HaleyHouse", 3, 7, normal)); //br
                Spots.Add(new Spot("Haley", "HaleyHouse", 8, 6, normal)); //br
            }
            if (Config.GetNPC("Haley").OtherSpots)
            {
                Spots.Add(new Spot("Haley", "HaleyHouse", 6, 15, rare)); //livingroom
                Spots.Add(new Spot("Haley", "Backwoods", 27, 27, very_rare)); //backwoods past bus stop
                Spots.Add(new Spot("Haley", "JoshHouse", 20, 6, very_rare)); //alex's room :o
            }
            npcName = "Haley";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Emily").HomeSpots)
            {
                Spots.Add(new Spot("Emily", "HaleyHouse", 19, 6, normal)); //br
                Spots.Add(new Spot("Emily", "HaleyHouse", 13, 8, normal)); //br
            }
            if (Config.GetNPC("Emily").OtherSpots)
            {
                Spots.Add(new Spot("Emily", "HaleyHouse", 5, 23, rare)); //livingroom
                Spots.Add(new Spot("Emily", "Beach", 4, 7, very_rare)); //beach, far left
            }
            npcName = "Emily";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                }
            }
            if (Config.GetNPC("Penny").HomeSpots)
            {
                Spots.Add(new Spot("Penny", "Trailer", 1, 8, normal)); //br
                Spots.Add(new Spot("Penny", "Trailer", 2, 10, normal)); //br
            }
            if (Config.GetNPC("Penny").OtherSpots)
            {
                Spots.Add(new Spot("Penny", "Trailer", 17, 8, rare)); //living room
                Spots.Add(new Spot("Penny", "Town", 59, 18, very_rare)); //by the community center, on the right
            }
            npcName = "Penny";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Jodi").HomeSpots)
            {
                Spots.Add(new Spot("Jodi", "SamHouse", 19, 7, normal)); //br
                Spots.Add(new Spot("Jodi", "SamHouse", 19, 6, normal)); //br
            }
            if (Config.GetNPC("Jodi").OtherSpots)
            {
                Spots.Add(new Spot("Jodi", "SamHouse", 8, 5, rare)); //kitchen
                Spots.Add(new Spot("Jodi", "Town", 44, 48, very_rare)); //behind the saloon, hidden
            }
            npcName = "Jodi";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Leah").HomeSpots)
            {
                Spots.Add(new Spot("Leah", "LeahHouse", 3, 6, normal)); //br
                Spots.Add(new Spot("Leah", "LeahHouse", 9, 7, normal)); //br
            }
            if (Config.GetNPC("Leah").OtherSpots)
            {
                Spots.Add(new Spot("Leah", "LeahHouse", 13, 14, rare)); //kitchen
                Spots.Add(new Spot("Leah", "Forest", 11, 7, very_rare)); //by log blocking combat forest
            }
            npcName = "Leah";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                }
            }
            if (Config.GetNPC("Caroline").HomeSpots)
            {
                Spots.Add(new Spot("Caroline", "SeedShop", 27, 5, normal)); //br
                Spots.Add(new Spot("Caroline", "SeedShop", 28, 8, normal)); //br
            }
            if (Config.GetNPC("Caroline").OtherSpots)
            {
                Spots.Add(new Spot("Caroline", "SeedShop", 24, 22, rare)); //big empty room
                Spots.Add(new Spot("Caroline", "SeedShop", 13, 28, very_rare)); //in store
            }
            npcName = "Caroline";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Abigail").HomeSpots)
            {
                Spots.Add(new Spot("Abigail", "SeedShop", 5, 8, normal)); //br
                Spots.Add(new Spot("Abigail", "SeedShop", 13, 6, normal)); //br
            }
            if (Config.GetNPC("Abigail").OtherSpots)
            {
                Spots.Add(new Spot("Abigail", "SeedShop", 40, 17, rare)); //altar
                Spots.Add(new Spot("Abigail", "SebastianRoom", 5, 5, very_rare)); //sebastian's room :o
            }
            npcName = "Abigail";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Maru").HomeSpots)
            {
                Spots.Add(new Spot("Maru", "ScienceHouse", 9, 7, normal)); //br
                Spots.Add(new Spot("Maru", "ScienceHouse", 5, 6, normal)); //br
            }
            if (Config.GetNPC("Maru").OtherSpots)
            {
                Spots.Add(new Spot("Maru", "ScienceHouse", 30, 12, rare)); //kitchen
                Spots.Add(new Spot("Maru", "Hospital", 20, 11, very_rare)); //at clinic
            }
            npcName = "Maru";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Robin").HomeSpots)
            {
                Spots.Add(new Spot("Robin", "ScienceHouse", 15, 4, normal)); //br
                Spots.Add(new Spot("Robin", "ScienceHouse", 19, 6, normal)); //br
            }
            if (Config.GetNPC("Robin").OtherSpots)
            {
                Spots.Add(new Spot("Robin", "ScienceHouse", 22, 19, rare)); //lab
                Spots.Add(new Spot("Robin", "Backwoods", 42, 10, very_rare)); //in woods, path behind her house
            }
            npcName = "Robin";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Alex").HomeSpots)
            {
                Spots.Add(new Spot("Alex", "JoshHouse", 13, 7, normal)); //br
                Spots.Add(new Spot("Alex", "JoshHouse", 17, 8, normal)); //br
            }
            if (Config.GetNPC("Alex").OtherSpots)
            {
                Spots.Add(new Spot("Alex", "Town", 73, 101, rare)); //town, by lower-right bridge
                Spots.Add(new Spot("Alex", "HaleyHouse", 4, 8, very_rare)); //haley's room
            }
            npcName = "Alex";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Harvey").HomeSpots)
            {
                Spots.Add(new Spot("Harvey", "HarveyRoom", 16, 6, normal)); //br
                Spots.Add(new Spot("Harvey", "HarveyRoom", 14, 5, normal)); //br
            }
            if (Config.GetNPC("Harvey").OtherSpots)
            {
                Spots.Add(new Spot("Harvey", "Hospital", 20, 11, rare)); //at clinic
                Spots.Add(new Spot("Harvey", "Hospital", 4, 14, very_rare)); //clinic, front counter
            }
            npcName = "Harvey";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
            if (Config.GetNPC("Sam").HomeSpots)
            {
                Spots.Add(new Spot("Sam", "SamHouse", 19, 15, normal)); //br
                Spots.Add(new Spot("Sam", "SamHouse", 13, 12, normal)); //br
            }
            if (Config.GetNPC("Sam").OtherSpots)
            {
                Spots.Add(new Spot("Sam", "Beach", 17, 13, rare)); //
                Spots.Add(new Spot("Sam", "ScienceHouse", 3, 6, very_rare)); //maru's room :o
            }
            npcName = "Sam";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 5, 51, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                }
            }
            if (Config.GetNPC("Sebastian").HomeSpots)
            {
                Spots.Add(new Spot("Sebastian", "SebastianRoom", 10, 7, normal)); //br
                Spots.Add(new Spot("Sebastian", "SebastianRoom", 8, 10, normal)); //br
            }
            if (Config.GetNPC("Sebastian").OtherSpots)
            {
                Spots.Add(new Spot("Sebastian", "Tunnel", 23, 7, rare)); //yep. teh tunnel.
                Spots.Add(new Spot("Sebastian", "SeedShop", 3, 8, very_rare)); //abigail's bedroom. :o
            }
            npcName = "Sebastian";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 6, 5, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 4, 4, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Shane").HomeSpots)
            {
                Spots.Add(new Spot("Shane", "AnimalShop", 28, 6, normal)); //br
                Spots.Add(new Spot("Shane", "AnimalShop", 22, 7, normal)); //br
            }
            if (Config.GetNPC("Shane").OtherSpots)
            {
                Spots.Add(new Spot("Shane", "Town", 101, 24, very_rare)); //above jojamart
                Spots.Add(new Spot("Shane", "AnimalShop", 2, 17, rare)); //by the fireplace
            }
            npcName = "Shane";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 24, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Clint").HomeSpots)
            {
                Spots.Add(new Spot("Clint", "Blacksmith", 9, 6, normal)); //br
                Spots.Add(new Spot("Clint", "Blacksmith", 3, 5, normal)); //br
            }
            if (Config.GetNPC("Clint").OtherSpots)
            {
                Spots.Add(new Spot("Clint", "Blacksmith", 9, 13, rare)); //by forge
                Spots.Add(new Spot("Clint", "ArchaeologyHouse", 42, 4, very_rare)); //wtf
            }
            npcName = "Clint";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 4, 28, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 2, 11, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 1, 25, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 15, 16, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 10, 26, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 15, 7, very_rare));
                }
            }
            if (Config.GetNPC("Demetrius").HomeSpots)
            {
                Spots.Add(new Spot("Demetrius", "ScienceHouse", 16, 7, normal)); //br
                Spots.Add(new Spot("Demetrius", "ScienceHouse", 14, 5, normal)); //br
            }
            if (Config.GetNPC("Demetrius").OtherSpots)
            {
                Spots.Add(new Spot("Demetrius", "ScienceHouse", 19, 22, rare)); //lab
                Spots.Add(new Spot("Demetrius", "Town", 31, 94, very_rare)); //hidden, by sewers
            }
            npcName = "Demetrius";
            if (Config.GetNPC(npcName).BathSpots)
            {
                Spots.Add(new Spot(npcName, "BathHouse_Entry", 1, 7, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 25, 18, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 23, 31, very_rare));
                Spots.Add(new Spot(npcName, "BathHouse_Pool", 10, 5, very_rare));
                Spots.Add(new Spot(npcName, "Railroad", 15, 57, very_rare));
                if (Config.GetNPC(npcName).IsFemale)
                {
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 11, 8, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_WomensLocker", 9, 24, very_rare));
                }
                else
                {
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 6, very_rare));
                    Spots.Add(new Spot(npcName, "BathHouse_MensLocker", 5, 14, very_rare));
                }
            }
        }

        public Spot(string npc, string loc, int x, int y, int chance)
        {
            this.NPC = npc;
            Location = loc;
            X = x;
            Y = y;
            PercentChance = chance;
        }
    }
}
