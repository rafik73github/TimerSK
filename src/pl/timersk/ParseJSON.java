package pl.timersk;


import org.json.JSONArray;
import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.format.TextStyle;
import java.util.*;

public class ParseJSON {

    private final JSONObject objectJSON;
    private final int success;
    private final boolean dataErr;
    private final boolean circuitVisit;
    private final String event;
    private final String sundayEvent;
    private final int openingComments;
    private final int treasuresTalk;
    private final int diggingForSpiritualGems;
    private final int livingOption;
    private final int livingPart1;
    private final int livingPart2;
    private final int congregationBibleStudy;
    private final int concludingComments;

    HttpHandler hh = new HttpHandler();


    public ParseJSON() throws Exception {
        Date dateNow = new Date();
        String modifiedDate= new SimpleDateFormat("yyyy-MM-dd").format(dateNow);

        JSONObject toSend = new JSONObject();
        toSend.put("token",null));

        objectJSON = new JSONObject(hh.sendPost(URLS.URL_MEETINGS,toSend));

        success = objectJSON.getInt("success");

        String date = objectJSON.getJSONObject("options").getString("date");
         dataErr = objectJSON.getJSONObject("options").getBoolean("dataErr");
         //dataErr = false;
         circuitVisit = objectJSON.getJSONObject("options").getBoolean("circuitVisit");
         event = objectJSON.getJSONObject("options").getString("event");

        sundayEvent = objectJSON.getJSONObject("options").getString("sundayEvent");

        openingComments = objectJSON.getJSONObject("meeting").getInt("openingComments");
        treasuresTalk = objectJSON.getJSONObject("meeting").getInt("treasuresTalk");
        diggingForSpiritualGems = objectJSON.getJSONObject("meeting").getInt("diggingForSpiritualGems");

        livingOption = objectJSON.getJSONObject("meeting").getInt("livingPartTwoPoint");

        livingPart1 = objectJSON.getJSONObject("meeting").getInt("livingPart1");
        livingPart2 = objectJSON.getJSONObject("meeting").getInt("livingPart2");
        congregationBibleStudy = objectJSON.getJSONObject("meeting").getInt("congregationBibleStudy");
        concludingComments = objectJSON.getJSONObject("meeting").getInt("concludingComments");

    }

    /**
     *
     * @return sorted meeting list
     */
    Map<Integer, Meetings> meetingArray(){
        Map<Integer, Meetings> result = new TreeMap<>();
        int id = 0;
        if(success == 1) {

            if (!dataErr) {
                if (checkIfWeekend()) {
                    if (!sundayEvent.equals("")) {

                        result.put(id, new Meetings(sundayEvent, 0));


                    } else {

                        if (circuitVisit) {
                            result.put(id, new Meetings("Wykład publiczny" ,30));

                            id++;
                            result.put(id, new Meetings("Studium Strażnicy" ,30));

                            id++;
                            result.put(id, new Meetings("Przemówienie nadzorcy obwodu" ,30));

                            id++;
                            result.put(id, new Meetings("Zbiórka do służby" ,7));

                        } else {

                            result.put(id, new Meetings("Wykład publiczny" ,30));

                            id++;
                            result.put(id, new Meetings("Studium Strażnicy" ,60));

                            id++;
                            result.put(id, new Meetings("Zbiórka do służby" ,7));

                        }
                    }//here
                } else {
                    //from here
                    if (!event.equals("")) {


                        result.put(id, new Meetings(event ,0));


                    } else {


                        result.put(id, new Meetings("Uwagi wstępne" ,openingComments));

                        id++;
                        result.put(id, new Meetings("Skarby ze Słowa Bożego" ,treasuresTalk));

                        id++;
                        result.put(id, new Meetings("Wyszukiwanie duchowych skarbów" ,diggingForSpiritualGems));

                        JSONArray arr = objectJSON.getJSONArray("tssk");
                        int len = arr.length();
                        for (int i = 0; i < len; i++) {
                            id++;
                            result.put(id, new Meetings(arr.getJSONObject(i).getString("pointName") ,arr.getJSONObject(i).getInt("pointTime")));

                        }

                        id++;
                        result.put(id, new Meetings("Chrześcijański tryb życia, cz. 1" ,livingPart1));

                        if (livingOption == 1) {
                            id++;
                            result.put(id, new Meetings("Chrześcijański tryb życia, cz. 2" ,livingPart2));

                           }
                        if (circuitVisit) {
                            id++;
                            result.put(id, new Meetings("Powtórka i zapowiedź następnego zebrania" ,concludingComments));

                            id++;
                            result.put(id, new Meetings("Przemówienie nadzorcy obwodu" ,30));

                        } else {
                            id++;
                            result.put(id, new Meetings("Zborowe studium Biblii" ,congregationBibleStudy));

                            id++;
                            result.put(id, new Meetings("Powtórka i zapowiedź następnego zebrania" ,concludingComments));

                       }
                    }
                    // to here
                }

            }  // dataErr
else{
    result = null;
            }
        }
        else{
            result = null;
        }
        return result;

    }



    /**
     * check if today is weekend day
     * @return true if weekend day, false if not
     */
    public boolean checkIfWeekend(){
        boolean results = false;
        LocalDate date = LocalDate.now();
        DayOfWeek dow = date.getDayOfWeek();
        String dayName = dow.getDisplayName(TextStyle.FULL, Locale.ENGLISH);
        if(dayName.equals("Sunday") || dayName.equals("Saturday")){
            results = true;
        }
        return results;
    }

}
