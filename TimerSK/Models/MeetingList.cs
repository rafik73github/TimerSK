using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TimerSK.Tools;
using TimerSK.Models;

namespace TimerSK
{

    //TODO add exception handling (if required)
    class MeetingList
    {
        //private readonly StreamReader reader;
        private readonly JObject json;
        private readonly JArray tssk;
        private readonly int tsskLength;
        private readonly List<(int minT, int secT, string pointName)> meetingList;
        private readonly int success;
        private readonly bool dataErr;
        private readonly string date;
        private readonly bool circuitVisit;
        private readonly string events;
        private readonly string sundayEvent;
        private readonly int openingComments;
        private readonly int treasuresTalk;
        private readonly int diggingForSpiritualGems;
        private readonly int livingPartTwoPoint;
        private readonly int livingPart1;
        private readonly int livingPart2;
        private readonly int congregationBibleStudy;
        private readonly int concludingComments;
        private readonly bool dataExist;

        public MeetingList()
        {

            json = JObject.Parse(new HTTPRequest().GetJsonFromAPI(HASH.HASH_VALUE));
            HASH.HASH_VALUE = "";
            meetingList = new List<(int minT, int secT, string pointName)> { };

            success = (int)json["success"];
            if (success == 1)
            {
                dataErr = (bool)json["options"]["dataErr"];
                date = (string)json["options"]["date"];
                circuitVisit = (bool)json["options"]["circuitVisit"];
                events = (string)json["options"]["event"];
                sundayEvent = (string)json["options"]["sundayEvent"];
                openingComments = (int)json["meeting"]["openingComments"];
                treasuresTalk = (int)json["meeting"]["treasuresTalk"];
                diggingForSpiritualGems = (int)json["meeting"]["diggingForSpiritualGems"];
                livingPartTwoPoint = (int)json["meeting"]["livingPartTwoPoint"];
                livingPart1 = (int)json["meeting"]["livingPart1"];
                livingPart2 = (int)json["meeting"]["livingPart2"];
                congregationBibleStudy = (int)json["meeting"]["congregationBibleStudy"];
                concludingComments = (int)json["meeting"]["concludingComments"];

                tssk = (JArray)json["tssk"];
                tsskLength = tssk.Count;
                dataExist = true;
            }
            else
            {
                dataExist = false;
            }
        }
        public List<(int minT, int secT, string pointName)> GetMeetingList()
        {
            if (dataExist)
            {
                if (!dataErr)
                {
                    if (TimeTools.IfWeekend())
                    {
                        if (!sundayEvent.Equals(""))
                        {
                            meetingList.Add((-1, -1, sundayEvent));
                        }
                        else
                        {
                            if (circuitVisit)
                            {
                                meetingList.Add((30, 0, "Wykład publiczny"));
                                meetingList.Add((30, 0, "Studium Strażnicy"));
                                meetingList.Add((30, 0, "Wykład nadzorcy obwodu"));
                                meetingList.Add((7, 0, "Zbiórka do służby"));
                            }
                            else
                            {
                                meetingList.Add((30, 0, "Wykład publiczny"));
                                meetingList.Add((60, 0, "Studium Strażnicy"));
                                meetingList.Add((7, 0, "Zbiórka do służby"));
                            }

                        }
                    }
                    else
                    {
                        if (!events.Equals(""))
                        {
                            meetingList.Add((0, 0, events));
                        }
                        else
                        {
                            meetingList.Add((openingComments, 0, "Uwagi wstępne"));
                            meetingList.Add((treasuresTalk, 0, "Skarby ze Słowa Bożego"));
                            meetingList.Add((diggingForSpiritualGems, 0, "Wyszukiwanie duchowych skarbów"));
                            for (int i = 0; i < tsskLength; i++)
                            {
                                meetingList.Add(((int)tssk[i]["pointTime"], 0, (string)tssk[i]["pointName"]));
                            }
                            if (livingPartTwoPoint == 0)
                            {
                                meetingList.Add((livingPart1, 0, "Chrześcijański tryb życia, cz. 1"));
                            }
                            else
                            {
                                meetingList.Add((livingPart1, 0, "Chrześcijański tryb życia, cz. 1"));
                                meetingList.Add((livingPart2, 0, "Chrześcijański tryb życia, cz. 2"));
                            }
                            if (circuitVisit)
                            {
                                meetingList.Add((concludingComments, 0, "Powtórka i zapowiedź następnego zebrania"));
                                meetingList.Add((30, 0, "Przemówienie nadzorcy obwodu"));
                            }
                            else
                            {
                                meetingList.Add((congregationBibleStudy, 0, "Zborowe studium Biblii"));
                                meetingList.Add((concludingComments, 0, "Powtórka i zapowiedź następnego zebrania"));
                            }
                        }

                    }

                }
                else
                {
                    meetingList.Add((-1, -1, "Błąd danych !"));
                }
            }
            else
            {
                meetingList.Add((-1, -1, "Błąd danych !"));
            }
            return meetingList;
        }


    }
}
