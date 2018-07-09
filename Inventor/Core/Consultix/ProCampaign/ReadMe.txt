Proximity.Dashboard.Utility.ProCampaign Wrapper
Release Notes 1.0.

Wrapper to procampaign rest api.

Web.Config Setup.

<configSections>
    <section name="ProCampaignClients" requirePermission="false" type="Proximity.Dashboard.Utility.ProCampaign.Configuration.ProCampaignRetrieverSection" />
</configSections>

<appSettings>
	<add key="ConsultixRestAPIUrl" value="https://consumer.procampaignapi.com/Consumer" />
	<add key="Environment" value="dev" />
	<!--<add key="Environment" value="stg" />
	<add key="Environment" value="prod" />-->
</appSettings>

<ProCampaignClients>
	<Clients>
		<add Locale="DE" Environment="stg" Key="xxx" />
		<add Locale="DE" Environment="prod" Key="yyy" />
		<add Locale="FR" Environment="stg" Key="zzz" />
	</Clients>
</ProCampaignClients>



---Testing Key Retrieval---

//both case insensitive
string key = ProCampaignKeyRepository.GetApiKey("DE", "stg");



--- Using Consumer Client Wrapper ---

//create new Instance of APIUtility
APIUtility transactionObject = new APIUtility();

//Build the Transaction
var rootObject = transactionObject.RootObject;
rootObject.Source = "CNFR151001_ChristmasGame_Sweepstake_CarteNoire_Campaign";
rootObject.Attributes.Add(new TransactionAttribute { Name = "Salutation", Value = "1" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Lastname", Value = "Lastname" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Firstname", Value = "Firstname" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Email", Value = "Email" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Birthday", Value = "Birthday" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Street1", Value = "Street1" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "Zipcode", Value = "Zipcode" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "City", Value = "City" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "PhonePrivate", Value = "PhonePrivate" });
rootObject.Attributes.Add(new TransactionAttribute { Name = "list:CarteNoire_Email", Value = "" });

//Set the transaction HttpVerb
transactionObject.Method = APIUtility.HttpVerb.POST;

//Provide additional Transaction parameters

rootObject.Transactions.Add(new Transaction
{
    Name = "CNFR151001 Carte Noire Christmas Game Sweepstake (IN)",
    Source = "CNFR151001_ChristmasGame_Sweepstake_CarteNoire_Campaign",
    Date_Created = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss"),
    Parameters = new List<TransactionParameter> { new TransactionParameter { Name = "Ident_long", Value = "ADF12A67-E97D-4C5E-86FB-85FD5BF00B18" } }
});

//Use Consumer Client Wrapper -- 
ProCampaign.Instance.GetConsumerClient("FR", "stg").Post(transactionObject);

// Variant of consumer client wrapper
//takes current environement for locale FR
ProCampaign.Instance.GetConsumerClient("FR").Post(transactionObject);