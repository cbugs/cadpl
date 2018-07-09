/* The deletes must be in this specific order, else constraints will PMS out on you! */
delete from ProCampaignStatus;
delete from ProCampaignTransactions;
delete from Entries;
delete from ParticipantDataCaches;
delete from Participants;
