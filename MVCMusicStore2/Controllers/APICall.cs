using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Services.Protocols;
using System.Net;

namespace APITestWebApp.WebServiceCode
{

    public class APICall
    {

        ResponsysWSService stub;
        bool loggedIn = false;
        SessionHeader sessionHeader;
        string hostname;
        bool show = true;



        public void triggerCampaignMessage(string email, string orderNumber)
        {
            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }
            string campaignName = "DOrderConfirmation";
            string folderName = "DK";
            string numOfRecipientsStr = "1";
            int numOfRecipients = 0;
            if (!"".Equals(numOfRecipientsStr))
            {
                numOfRecipients = Convert.ToInt16(numOfRecipientsStr);
            }
            RecipientData[] recipientData = null;
            if (numOfRecipients > 0)
            {
                recipientData = new RecipientData[numOfRecipients];
            }
            else
            {
                recipientData = new RecipientData[1];
                recipientData[0] = null;
            }
            for (int i = 0; i < numOfRecipients; i++)
            {
               // sop("***********************");
                recipientData[i] = new RecipientData();
                long riid = 0;
                string riidStr = "";
                if (riidStr != null && !"".Equals(riidStr))
                {
                    riid = Convert.ToInt64(riidStr);
                }
                string emailAddress = email;
                string customerId = "";//getUserInput("Enter the Customer Id : ");
                string mobileNumber = "";//getUserInput("Enter the Mobile Number : ");
                string format = "HTML_FORMAT";//getUserInput("Enter the Email Format (TEXT_FORMAT, HTML_FORMAT, MULTIPART_FORMAT, NO_FORMAT) : ");
                EmailFormat emailFormat = EmailFormat.NO_FORMAT;
                if (!"".Equals(format))
                    emailFormat = (EmailFormat)Enum.Parse(typeof(EmailFormat), format, true);
                Recipient recipient = new Recipient();
                InteractObject listName = new InteractObject();
                listName.folderName = "";
                listName.objectName = "";
                recipient.listName = listName;
                recipient.recipientId = riid;
                recipient.emailAddress = emailAddress;
                recipient.customerId = customerId;
                recipient.mobileNumber = mobileNumber;
                recipient.emailFormat = emailFormat;
                string numOfVariablesStr = "1";//getUserInput("How many campaign variables you want to specify : ");
                int numOfVariables = 0;
                if (!"".Equals(numOfVariablesStr))
                    numOfVariables = Convert.ToInt16(numOfVariablesStr);

                OptionalData[] variables = null;
                if (numOfVariables > 0)
                {
                    variables = new OptionalData[numOfVariables];
                }
                else
                {
                    variables = new OptionalData[1];
                    variables[0] = null;
                }
                for (int j = 0; j < numOfVariables; j++)
                {
                    //sop("***********************");
                    string name = "POINTS";
                    string value = orderNumber;//getUserInput("Enter the value of the variable : ");

                    variables[j] = new OptionalData();
                    variables[j].name = name;
                    variables[j].value = value;
                }
                recipientData[i].recipient = recipient;
                recipientData[i].optionalData = variables;
            }
            try
            {
                InteractObject campaign = new InteractObject();
                campaign.folderName = folderName;
                campaign.objectName = campaignName;
                TriggerResult[] trgMsgResults = stub.triggerCampaignMessage(campaign, recipientData);
                if (trgMsgResults != null)
                {
                   // sop("**************************************");
                    int i = 1;
                    foreach (TriggerResult result in trgMsgResults)
                    {
                        if (result != null)
                        {
                          /*  sop("Trigger Campaign Message Successful");
                            sop("**********************************************");
                            sop("Results for Recipient " + i++);
                            sop("Recipient Id : " + result.recipientId);
                            sop("Success : " + result.success);
                            sop("Error Message : " + result.errorMessage);
                            sop("**********************************************");*/
                        }
                        else
                        {
                           /* sop("**************************************");
                            sop("Trigger Campaign Message Failed");
                            sop("**************************************");*/
                        }
                    }
                  /*  sop("**************************************");*/
                }
                else
                {
                  /*  sop("**************************************");
                    sop("Trigger Campaign Message Failed");
                    sop("**************************************");*/
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in triggerCampaignMessage : " + e.Message);
                Console.WriteLine("SoapException in triggerCampaignMessage : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in triggerCampaignMessage : " + e.Message);
            }
        }

        //**************************************************************************************************************
        //**************************************************************************************************************
        public void triggerCustomEvent(string email)
        {

            string eventName = "QSU";
            string eventIdStr = "6225";

            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }
            // string eventName = getUserInput("Enter the Custom Event Name you want to trigger : ");
            //  string eventIdStr = getUserInput("Enter the Custom Event Id for the custom event : ");
            long eventId = 0;
            if (eventIdStr != null && !eventIdStr.Equals(""))
            {
                eventId = Convert.ToInt64(eventIdStr);
            }
            //string strMapCol = getUserInput("Enter the string mapping column name for the custom event : ");
            //string numMapCol = getUserInput("Enter the number mapping column name for the custom event : ");
            //string datMapCol = getUserInput("Enter the date mapping column name for the custom event : ");
            CustomEvent customEvent = new CustomEvent();
            customEvent.eventName = eventName;
            customEvent.eventId = eventId;
            customEvent.eventStringDataMapping = ""; //strMapCol;
            customEvent.eventNumberDataMapping = "";//numMapCol;
            customEvent.eventDateDataMapping = "";//datMapCol;
            string numOfRecipientsStr = "1";
            int numOfRecipients = 0;
            if (!"".Equals(numOfRecipientsStr))
                numOfRecipients = Convert.ToInt16(numOfRecipientsStr);

            RecipientData[] recipientData = null;
            if (numOfRecipients > 0)
            {
                recipientData = new RecipientData[numOfRecipients];
            }
            else
            {
                recipientData = new RecipientData[1];
                recipientData[0] = null;
            }
            for (int i = 0; i < numOfRecipients; i++)
            {
                recipientData[i] = new RecipientData();
                long riid = 0;
                string folderName = "DK";
                string listName = "DK_CONTACTS_LIST";

                string riidStr = "";
                  if (riidStr != null && !"".Equals(riidStr))
                  {
                      riid = Convert.ToInt64(riidStr);
                  }
                string emailAddress = email;
                string customerId = "";
                string mobileNumber = "";
              //  string points = "";
                Recipient recipient = new Recipient();
                InteractObject listObj = new InteractObject();
                listObj.folderName = folderName;
                listObj.objectName = listName;
                recipient.listName = listObj;
                recipient.recipientId = riid;
                recipient.emailAddress = emailAddress;
                recipient.customerId = customerId;
                recipient.mobileNumber = mobileNumber;
                recipient.emailFormat = EmailFormat.NO_FORMAT;
                //recipient.
                 string numOfVariablesStr = "";
                    int numOfVariables = 0;
                    if (!"".Equals(numOfVariablesStr))
                        numOfVariables = Convert.ToInt16(numOfVariablesStr);

                    OptionalData[] enactmentData = null;
                    if (numOfVariables > 0)
                    {
                        enactmentData = new OptionalData[numOfVariables];
                    }
                    else
                    {
                        enactmentData = new OptionalData[1];
                        enactmentData[0] = null;
                    }
                    for (int j = 0; j < numOfVariables; j++)
                    {
                        string name = "";
                        string value = "";

                        enactmentData[j] = new OptionalData();
                        enactmentData[j].name = name;
                        enactmentData[j].value = value;
                    }
                recipientData[i].recipient = recipient;
                recipientData[i].optionalData = enactmentData;
            }
            try
            {
                TriggerResult[] custEvtResults = stub.triggerCustomEvent(customEvent, recipientData);

                if (custEvtResults != null)
                {
                    // sop("**************************************");
                    int i = 1;
                    foreach (TriggerResult result in custEvtResults)
                    {
                        if (result != null)
                        {
                            /* sop("Trigger Custom Event Successful");
                             sop("**********************************************");
                             sop("Results for Recipient " + i++);
                             sop("Recipient Id : " + result.recipientId);
                             sop("Success : " + result.success);
                             sop("Error Message : " + result.errorMessage);
                             sop("**********************************************");*/
                        }
                        else
                        {
                            /* sop("**************************************");
                             sop("Trigger Custom Event Failed");
                             sop("**************************************");*/
                        }
                    }
                    /* sop("**************************************");*/
                }
                else
                {
                    /*sop("**************************************");
                    sop("Trigger Custom Event Failed");
                    sop("**************************************");*/
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in triggerCustomEvent : " + e.Message);
                Console.WriteLine("SoapException in triggerCustomEvent : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in triggerCustomEvent : " + e.Message);
            }
        }
        public void triggerCustomEvent(string email,string points)
        {

            string eventName = "PURCHASED";
            string eventIdStr = "6245";

            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }
            // string eventName = getUserInput("Enter the Custom Event Name you want to trigger : ");
            //  string eventIdStr = getUserInput("Enter the Custom Event Id for the custom event : ");
            long eventId = 0;
            if (eventIdStr != null && !eventIdStr.Equals(""))
            {
                eventId = Convert.ToInt64(eventIdStr);
            }
            //string strMapCol = getUserInput("Enter the string mapping column name for the custom event : ");
            //string numMapCol = getUserInput("Enter the number mapping column name for the custom event : ");
            //string datMapCol = getUserInput("Enter the date mapping column name for the custom event : ");
            CustomEvent customEvent = new CustomEvent();
            customEvent.eventName = eventName;
            customEvent.eventId = eventId;
            customEvent.eventStringDataMapping = ""; //strMapCol;
            customEvent.eventNumberDataMapping = "";//numMapCol;
            customEvent.eventDateDataMapping = "";//datMapCol;
            string numOfRecipientsStr = "1";
            int numOfRecipients = 0;
            if (!"".Equals(numOfRecipientsStr))
                numOfRecipients = Convert.ToInt16(numOfRecipientsStr);

            RecipientData[] recipientData = null;
            if (numOfRecipients > 0)
            {
                recipientData = new RecipientData[numOfRecipients];
            }
            else
            {
                recipientData = new RecipientData[1];
                recipientData[0] = null;
            }
            for (int i = 0; i < numOfRecipients; i++)
            {
                recipientData[i] = new RecipientData();
                long riid = 0;
                string folderName = "DK";
                string listName = "DK_CONTACTS_LIST";

                string riidStr = "";
                if (riidStr != null && !"".Equals(riidStr))
                {
                    riid = Convert.ToInt64(riidStr);
                }
                string emailAddress = email;
                string customerId = "";
                string mobileNumber = "";
               // string points = "";
                Recipient recipient = new Recipient();
                InteractObject listObj = new InteractObject();
                listObj.folderName = folderName;
                listObj.objectName = listName;
                recipient.listName = listObj;
                recipient.recipientId = riid;
                recipient.emailAddress = emailAddress;
                recipient.customerId = customerId;
                recipient.mobileNumber = mobileNumber;
                recipient.emailFormat = EmailFormat.NO_FORMAT;
                //recipient.
                string numOfVariablesStr = "1";
                int numOfVariables = 0;
                if (!"".Equals(numOfVariablesStr))
                    numOfVariables = Convert.ToInt16(numOfVariablesStr);

                OptionalData[] enactmentData = null;
                if (numOfVariables > 0)
                {
                    enactmentData = new OptionalData[numOfVariables];
                }
                else
                {
                    enactmentData = new OptionalData[1];
                    enactmentData[0] = null;
                }
                for (int j = 0; j < numOfVariables; j++)
                {
                    string name = "POINTS";
                    string value = points;

                    enactmentData[j] = new OptionalData();
                    enactmentData[j].name = name;
                    enactmentData[j].value = value;
                }
                recipientData[i].recipient = recipient;
                recipientData[i].optionalData = enactmentData;
            }
            try
            {
                TriggerResult[] custEvtResults = stub.triggerCustomEvent(customEvent, recipientData);

                if (custEvtResults != null)
                {
                    // sop("**************************************");
                    int i = 1;
                    foreach (TriggerResult result in custEvtResults)
                    {
                        if (result != null)
                        {
                            /* sop("Trigger Custom Event Successful");
                             sop("**********************************************");
                             sop("Results for Recipient " + i++);
                             sop("Recipient Id : " + result.recipientId);
                             sop("Success : " + result.success);
                             sop("Error Message : " + result.errorMessage);
                             sop("**********************************************");*/
                        }
                        else
                        {
                            /* sop("**************************************");
                             sop("Trigger Custom Event Failed");
                             sop("**************************************");*/
                        }
                    }
                    /* sop("**************************************");*/
                }
                else
                {
                    /*sop("**************************************");
                    sop("Trigger Custom Event Failed");
                    sop("**************************************");*/
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in triggerCustomEvent : " + e.Message);
                Console.WriteLine("SoapException in triggerCustomEvent : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in triggerCustomEvent : " + e.Message);
            }
        }


        public bool login()
        {
            bool result = false;
            try
            {
                string url = "https://" + "ws5.responsys.net" + "/webservices/services/ResponsysWSService";
                //  Console.WriteLine("Web Services URL = " + url);

                string userName = "api-dk";
                string passWord = "d3ltaF0rc3";


                stub = new ResponsysWSService();
                stub.CookieContainer = new CookieContainer();
                stub.Url = url;

                LoginResult loginResult = stub.login(userName, passWord);
                string sessionId = loginResult.sessionId;

                /*     sop("**************************************");
                     sop("Login Result = " + (sessionId != null));
                     sop("**************************************");*/
                if (sessionId != null)
                {
                    sessionHeader = new SessionHeader();
                    sessionHeader.sessionId = sessionId;
                    stub.SessionHeaderValue = sessionHeader;

                    /*  sop("Setting the Client Timeout to 1 hour");*/

                    // Set timeout
                    stub.Timeout = 1000 * 60 * 60;
                    loggedIn = true;
                    result = true;


                }



            }

            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in login : " + e.Message);
                Console.WriteLine("SoapException in login : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in login : " + e.Message);
            }

            return result;

        }

        public void logout()
        {
            try
            {
                if (!loggedIn)
                {
                    if (!login())
                    {
                        return;
                    }
                }
                bool loggedOut = stub.logout();
                loggedIn = false;

            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in logout : " + e.Message);
                Console.WriteLine("SoapException in logout : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in logout : " + e.Message);
            }
        }

        public void mergeListMembers(string uList, string folders, string email, string fname, string lname)
        {
            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }

            string listName = uList;
            string folderName = folders;

            string numOfFieldNamesStr = "3";
            int numOfFieldNames = 0;
            if (!"".Equals(numOfFieldNamesStr))
            {
                numOfFieldNames = Convert.ToInt16(numOfFieldNamesStr);
            }
            string[] fieldNames = null;
            if (numOfFieldNames > 0)
            {
                fieldNames = new string[numOfFieldNames];

                fieldNames[0] = "EMAIL_ADDRESS_";
                fieldNames[1] = "FIRST_NAME";
                fieldNames[2] = "LAST_NAME";
              //  fieldNames[3] = "EMAIL_PERMISSION_STATUS_";


            }
            else
            {
                fieldNames = new string[1];
                fieldNames[0] = null;
            }

            string numOfRecordsStr = "1";
            int numOfRecords = 0;
            if (!"".Equals(numOfRecordsStr))
            {
                numOfRecords = Convert.ToInt16(numOfRecordsStr);
            }
            Record[] records = null;
            if (numOfRecords > 0)
            {
                records = new Record[numOfRecords];
                records[0] = new Record();
                string[] fieldValues = new string[numOfFieldNames];

                //assign values to the fields
                fieldValues[0] = email; // Customer_ID_ value
                fieldValues[1] = fname; //Email_Address_ Value
                fieldValues[2] = lname; //FirstName Value
                //fieldNames[3] = PermStatus; // EMAIL_PERMISSION_STATUS_
                records[0].fieldValues = fieldValues;
                /*  records = new Record[numOfRecords];
                  for (int i = 0; i < numOfRecords; i++)
                  {
                      sop("Record " + i);
                      records[i] = new Record();
                      string[] fieldValues = new string[numOfFieldNames];
                      sop("*************************************************************************");
                      sop("Enter the field values in the order of field names you've specified above");
                      sop("*************************************************************************");
                      for (int j = 0; j < numOfFieldNames; j++)
                      {
                          fieldValues[j] = getUserInput("Enter the Field Value " + fieldNames[j] + " : ");
                      }
                      records[i].fieldValues = fieldValues;
                  }*/
            }
            else
            {
                records = new Record[1];
                records[0] = null;
            }
            RecordData recordData = new RecordData();
            recordData.fieldNames = fieldNames;
            recordData.records = records;

            ListMergeRule listMergeRule = new ListMergeRule();

            bool insertOnNoMatch = true;//"Y".Equals(getUserInput("Do you want to insert the record if match is not found (Y/N) : "));
            string updateOnMatchStr = "REPLACE_ALL";//getUserInput("Enter the update rule if record is found (REPLACE_ALL, NO_UPDATE, REPLACE_IF_EXISTING_BLANK, REPLACE_IF_NEW_BLANK) : ");
            UpdateOnMatch updateOnMatch = (UpdateOnMatch)Enum.Parse(typeof(UpdateOnMatch), updateOnMatchStr, true);
            string matchColumnName1 = "EMAIL_ADDRESS_";//getUserInput("Enter the match column name 1 for the merge : ");
            //string matchColumnName2 = getUserInput("Enter the match column name 2 for the merge : ");
            //string matchColumnName3 = getUserInput("Enter the match column name 3 for the merge : ");
            string matchOpStr = "NONE";//getUserInput("Enter the match operator (NONE, AND, OR) : ");
            MatchOperator matchOperator = (MatchOperator)Enum.Parse(typeof(MatchOperator), matchOpStr, true);
            string optinValue = "I";//getUserInput("Enter the value for Opt-In : ");
            string optoutValue = "O";//getUserInput("Enter the value for Opt-Out : ");
            string htmlValue = "H"; //getUserInput("Enter the value for HTML Format : ");
            string textValue = "T";//getUserInput("Enter the value for TEXT Format : ");
            //string rejectRecordIfChannelEmpty = //getUserInput("Enter the channel you want to reject is the value is empty. Enter E for email, M for Mobile & P for Postal (for multiple channels put a comma between the channels): ");
            string defPermStatusStr = "OPTIN";//getUserInput("Enter the default permission status (OPTIN, OPTOUT) : ");
            PermissionStatus defPermStatus = (PermissionStatus)Enum.Parse(typeof(PermissionStatus), defPermStatusStr, true);

            listMergeRule.insertOnNoMatch = insertOnNoMatch;
            listMergeRule.updateOnMatch = updateOnMatch;
            listMergeRule.matchColumnName1 = matchColumnName1;
            //  listMergeRule.matchColumnName2 = matchColumnName2;
            // listMergeRule.matchColumnName3 = matchColumnName3;
            listMergeRule.matchOperator = matchOperator;
            listMergeRule.optinValue = optinValue;
            listMergeRule.optoutValue = optoutValue;
            listMergeRule.htmlValue = htmlValue;
            listMergeRule.textValue = textValue;
            //  listMergeRule.rejectRecordIfChannelEmpty = rejectRecordIfChannelEmpty;
            listMergeRule.defaultPermissionStatus = defPermStatus;
            //listMergeRule.setTestRecipient(testRecipient);

            try
            {
                InteractObject list = new InteractObject();
                list.folderName = folderName;
                list.objectName = listName;
                MergeResult mergeResult = stub.mergeListMembers(list, recordData, listMergeRule);
                //   stub.ses

                if (mergeResult != null)
                {
                    /* sop("Merge Into List Successful");
                     sop("Insert Count   = " + mergeResult.insertCount);
                     sop("Update Count   = " + mergeResult.updateCount);
                     sop("Rejected Count = " + mergeResult.rejectedCount);
                     sop("Total Count    = " + mergeResult.totalCount);
                     sop("Error Message  = " + mergeResult.errorMessage);*/
                }
                else
                {
                    /* sop("************************************");
                     sop("Merge Into List Failed");
                     sop("************************************");
                     * */
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in mergeListMembers : " + e.Message);
                Console.WriteLine("SoapException in mergeListMembers : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in mergeListMembers : " + e.Message);
            }
        }

        public void mergeListMembers(string uList, string folders, string email, string fname, string lname, string PermStatus)
        {
            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }

            string listName = uList;
            string folderName = folders;

            string numOfFieldNamesStr = "4";
            int numOfFieldNames = 0;
            if (!"".Equals(numOfFieldNamesStr))
            {
                numOfFieldNames = Convert.ToInt16(numOfFieldNamesStr);
            }
            string[] fieldNames = null;
            if (numOfFieldNames > 0)
            {
                fieldNames = new string[numOfFieldNames];

                fieldNames[0] = "EMAIL_ADDRESS_";
                fieldNames[1] = "FIRST_NAME";
                fieldNames[2] = "LAST_NAME";
                fieldNames[3] = "EMAIL_PERMISSION_STATUS_";


            }
            else
            {
                fieldNames = new string[1];
                fieldNames[0] = null;
            }

            string numOfRecordsStr = "1";
            int numOfRecords = 0;
            if (!"".Equals(numOfRecordsStr))
            {
                numOfRecords = Convert.ToInt16(numOfRecordsStr);
            }
            Record[] records = null;
            if (numOfRecords > 0)
            {
                records = new Record[numOfRecords];
                records[0] = new Record();
                string[] fieldValues = new string[numOfFieldNames];

                //assign values to the fields
                fieldValues[0] = email; // Customer_ID_ value
                fieldValues[1] = fname; //Email_Address_ Value
                fieldValues[2] = lname; //FirstName Value
                fieldValues[3] = PermStatus;
                records[0].fieldValues = fieldValues;
                /*  records = new Record[numOfRecords];
                  for (int i = 0; i < numOfRecords; i++)
                  {
                      sop("Record " + i);
                      records[i] = new Record();
                      string[] fieldValues = new string[numOfFieldNames];
                      sop("*************************************************************************");
                      sop("Enter the field values in the order of field names you've specified above");
                      sop("*************************************************************************");
                      for (int j = 0; j < numOfFieldNames; j++)
                      {
                          fieldValues[j] = getUserInput("Enter the Field Value " + fieldNames[j] + " : ");
                      }
                      records[i].fieldValues = fieldValues;
                  }*/
            }
            else
            {
                records = new Record[1];
                records[0] = null;
            }
            RecordData recordData = new RecordData();
            recordData.fieldNames = fieldNames;
            recordData.records = records;

            ListMergeRule listMergeRule = new ListMergeRule();

            bool insertOnNoMatch = true;//"Y".Equals(getUserInput("Do you want to insert the record if match is not found (Y/N) : "));
            string updateOnMatchStr = "REPLACE_ALL";//getUserInput("Enter the update rule if record is found (REPLACE_ALL, NO_UPDATE, REPLACE_IF_EXISTING_BLANK, REPLACE_IF_NEW_BLANK) : ");
            UpdateOnMatch updateOnMatch = (UpdateOnMatch)Enum.Parse(typeof(UpdateOnMatch), updateOnMatchStr, true);
            string matchColumnName1 = "EMAIL_ADDRESS_";//getUserInput("Enter the match column name 1 for the merge : ");
            //string matchColumnName2 = getUserInput("Enter the match column name 2 for the merge : ");
            //string matchColumnName3 = getUserInput("Enter the match column name 3 for the merge : ");
            string matchOpStr = "NONE";//getUserInput("Enter the match operator (NONE, AND, OR) : ");
            MatchOperator matchOperator = (MatchOperator)Enum.Parse(typeof(MatchOperator), matchOpStr, true);
            string optinValue = "I";//getUserInput("Enter the value for Opt-In : ");
            string optoutValue = "O";//getUserInput("Enter the value for Opt-Out : ");
            string htmlValue = "H"; //getUserInput("Enter the value for HTML Format : ");
            string textValue = "T";//getUserInput("Enter the value for TEXT Format : ");
            //string rejectRecordIfChannelEmpty = //getUserInput("Enter the channel you want to reject is the value is empty. Enter E for email, M for Mobile & P for Postal (for multiple channels put a comma between the channels): ");
            string defPermStatusStr = "OPTOUT";//getUserInput("Enter the default permission status (OPTIN, OPTOUT) : ");
            PermissionStatus defPermStatus = (PermissionStatus)Enum.Parse(typeof(PermissionStatus), defPermStatusStr, true);

            listMergeRule.insertOnNoMatch = insertOnNoMatch;
            listMergeRule.updateOnMatch = updateOnMatch;
            listMergeRule.matchColumnName1 = matchColumnName1;
            //  listMergeRule.matchColumnName2 = matchColumnName2;
            // listMergeRule.matchColumnName3 = matchColumnName3;
            listMergeRule.matchOperator = matchOperator;
            listMergeRule.optinValue = optinValue;
            listMergeRule.optoutValue = optoutValue;
            listMergeRule.htmlValue = htmlValue;
            listMergeRule.textValue = textValue;
            //  listMergeRule.rejectRecordIfChannelEmpty = rejectRecordIfChannelEmpty;
            listMergeRule.defaultPermissionStatus = defPermStatus;
            //listMergeRule.setTestRecipient(testRecipient);

            try
            {
                InteractObject list = new InteractObject();
                list.folderName = folderName;
                list.objectName = listName;
                MergeResult mergeResult = stub.mergeListMembers(list, recordData, listMergeRule);
                //   stub.ses

                if (mergeResult != null)
                {
                    /* sop("Merge Into List Successful");
                     sop("Insert Count   = " + mergeResult.insertCount);
                     sop("Update Count   = " + mergeResult.updateCount);
                     sop("Rejected Count = " + mergeResult.rejectedCount);
                     sop("Total Count    = " + mergeResult.totalCount);
                     sop("Error Message  = " + mergeResult.errorMessage);*/
                }
                else
                {
                    /* sop("************************************");
                     sop("Merge Into List Failed");
                     sop("************************************");
                     * */
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in mergeListMembers : " + e.Message);
                Console.WriteLine("SoapException in mergeListMembers : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in mergeListMembers : " + e.Message);
            }
        }
   
        public void retrieveListMembers(string uList, string folders, string numOfFields, string[] fieldNames, string email)
        {
            if (!loggedIn)
            {
                if (!login())
                {
                    return;
                }
            }

            string listName = uList;
            string folderName = folders;

            string numOfFieldListStr = numOfFields;
            int numOfFieldList = 0;
            if (!"".Equals(numOfFieldListStr))
            {
                numOfFieldList = Convert.ToInt16(numOfFieldListStr);
            }
            string[] fieldList = null;
            if (numOfFieldList > 0)
            {
                fieldList = new string[numOfFieldList];
                for (int i = 0; i < numOfFieldList; i++)
                {
                    fieldList[i] = fieldNames[i];
                }
            }
            else
            {
                fieldList = new string[1];
                fieldList[0] = null;
            }

            string qryColStr = "EMAIL_ADDRESS";
            QueryColumn queryColumn = (QueryColumn)Enum.Parse(typeof(QueryColumn), qryColStr, true);

            string numOfIdsStr = "1";
            int numOfIds = 0;
            if (!"".Equals(numOfIdsStr))
            {
                numOfIds = Convert.ToInt16(numOfIdsStr);
            }
            string[] idsToRetrieve = null;
            if (numOfIds > 0)
            {
                idsToRetrieve = new string[numOfIds];
                for (int i = 0; i < numOfIds; i++)
                {
                    idsToRetrieve[i] = email;
                }
            }
            else
            {
                idsToRetrieve = new string[1];
                idsToRetrieve[0] = null;
            }

            try
            {
                InteractObject list = new InteractObject();
                list.folderName = folderName;
                list.objectName = listName;
                RetrieveResult retrieveResult = stub.retrieveListMembers(list, queryColumn, fieldList, idsToRetrieve);

                RecordData recordData = retrieveResult.recordData;
                if (recordData != null)
                {
                    //  sop("****************************************");
                    // sop("Retrieve List Recipients Successful");
                    int i = 1;
                    Record[] records = recordData.records;
                    foreach (Record record in records)
                    {
                        string[] values1 = record.fieldValues;
                        string[] value2 = recordData.fieldNames;
              //          Controllers.HomeController.valuePair = record.fieldValues; look at this line
                    }
                    /*   foreach (Record record in records)
                       {
                             sop("Record " + i);
                               sop("*****************************");
                             printStringArray(record.fieldValues, recordData.fieldNames);
                               sop("*****************************");
                               i++;
                           }
                           sop("****************************************"); 
                       }*/
                }
                else
                {
                    /*sop("************************************");
                      sop("Retrieve List Recipients Failed");
                      sop("************************************");*/
                }
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Console.WriteLine("SoapException in retrieveListMembers : " + e.Message);
                Console.WriteLine("SoapException in retrieveListMembers : " + e.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in retrieveListMembers : " + e.Message);
            }
        }


    }
}