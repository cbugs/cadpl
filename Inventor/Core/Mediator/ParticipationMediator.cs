using Cadbury.Inventor.Core.Domain.Entities;
using Cadbury.Inventor.Core.Domain.Manager;
using Cadbury.Inventor.Core.DTO;
using Cadbury.Inventor.Core.Mediator.Models;
using Cadbury.Inventor.Core.Module;
using Cadbury.Inventor.Core.Utils;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using Kaliko.ImageLibrary;
using System.Web.Hosting;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Web;
using System.Text;

namespace Cadbury.Inventor.Core.Mediator
{
    public class ParticipationMediator : MediatorBase<ParticipationMediatorModel>, IMediator
    {
        public ParticipationMediator(IMediatorModel mediatorModel)
            : base(mediatorModel)
        {
        }

        private string GetRequestApplicationPath()
        {
            string requestPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            return HostingEnvironment.MapPath(requestPath);
        }

        // WARNING: Very ugly code ahead, quick and dirty method to generate an image
        private Dictionary<string, string> BuildCDMBar(string barName, string barNameBackgroundColour, string ingredient1, string ingredient2, string ingredient3, string directoryId, string userImagesPath)
        {
            // Locate base directory for CDM bar assets
            string cdmBarAssetsPath = Path.Combine(GetRequestApplicationPath(), "App_Data", "cdm_bar");

            var fileId = Guid.NewGuid().ToString("N");
            string imageFilePath = $"{directoryId}/{fileId}-1.jpg";
            //string imageShareFilePath = $"{directoryId}/{fileId}-2.jpg";
            string imageShareWideFilePath = $"{directoryId}/{fileId}-3.jpg";

            // Count the amount of ingredients
            int ingredientAmount = 0;
            ingredientAmount += !String.IsNullOrEmpty(ingredient1) ? 1 : 0;
            ingredientAmount += !String.IsNullOrEmpty(ingredient2) ? 1 : 0;
            ingredientAmount += !String.IsNullOrEmpty(ingredient3) ? 1 : 0;

            // Create a new canvas based on background image (helps in getting a dynamic dimension also)
            String backgroundImagePath = Path.Combine(cdmBarAssetsPath, "background.png");
            using (var image = new KalikoImage(backgroundImagePath))
            {

                // Composite the image with ingredients by blitting, yeah too many nested IFs
                if (ingredientAmount > 0)
                {
                    var ingredient1ImagePath = Path.Combine(cdmBarAssetsPath, ingredient1.ToLower());
                    using (var ingredientImage = new KalikoImage(ingredient1ImagePath))
                    {
                        // Very important to set the image to the same resolution as the exported file, else proportions won't be maintained
                        ingredientImage.SetResolution(96, 96);
                        image.BlitImage(ingredientImage, 2596, 1090);
                    }

                    if (ingredientAmount > 1)
                    {
                        var ingredient2ImagePath = Path.Combine(cdmBarAssetsPath, ingredient2.ToLower());
                        using (var ingredientImage = new KalikoImage(ingredient2ImagePath))
                        {
                            // Very important to set the image to the same resolution as the exported file, else proportions won't be maintained
                            ingredientImage.SetResolution(96, 96);
                            image.BlitImage(ingredientImage, 2646, 698);
                        }

                        if (ingredientAmount > 2)
                        {
                            var ingredient3ImagePath = Path.Combine(cdmBarAssetsPath, ingredient3.ToLower());
                            using (var ingredientImage = new KalikoImage(ingredient3ImagePath))
                            {
                                // Very important to set the image to the same resolution as the exported file, else proportions won't be maintained
                                ingredientImage.SetResolution(96, 96);
                                image.BlitImage(ingredientImage, 2390, 392);
                            }
                        }
                    }
                }

                // Overlay the CDM Bar with "masking" holes
                var cdmBarImagePath = Path.Combine(cdmBarAssetsPath, $"bar-ingredient-x{ingredientAmount.ToString()}.png");
                using (var cdmBarImage = new KalikoImage(cdmBarImagePath))
                {
                    cdmBarImage.SetResolution(96, 96);
                    image.BlitImage(cdmBarImage);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Overlay the text background if any
                if (!(String.IsNullOrEmpty(barNameBackgroundColour) || barNameBackgroundColour.ToLower().Equals("transparent")))
                {
                    var barNameBackgroundImagePath = Path.Combine(cdmBarAssetsPath, $"back-{barNameBackgroundColour.ToLower()}.png");
                    using (var barNameBackgroundImage = new KalikoImage(barNameBackgroundImagePath))
                    {
                        barNameBackgroundImage.SetResolution(96, 96);
                        image.BlitImage(barNameBackgroundImage);
                    }
                }

                // Add the final touch, the "pièce de résistance", the centered angled text!
                var fontPath = Path.Combine(cdmBarAssetsPath, "MonkyBasicRegular.ttf");
                image.SetFont(fontPath, 110, FontStyle.Regular);
                var text = new TextField(barName.ToUpper());
                text.Rotation = -14.1f;
                text.TextColor = Color.White;
                text.Alignment = StringAlignment.Center;
                text.VerticalAlignment = StringAlignment.Center;
                text.TargetArea = new Rectangle(900, 1231, 1800, 618);
                image.DrawText(text);

                // Create destination directory if it doesn't already exist
                String destinationDirectoryPath = Path.Combine(userImagesPath, directoryId);
                System.IO.Directory.CreateDirectory(destinationDirectoryPath);

                // Create the share image (square aspect ratio)
                /*
                String backgroundShareImagePath = Path.Combine(cdmBarAssetsPath, "background-share.png");
                using (var shareImage = new KalikoImage(backgroundShareImagePath))
                {
                    shareImage.SetResolution(96, 96);
                    shareImage.BlitImage(image);

                    // Save the square share image                
                    String destinationShareFilePath = Path.Combine(userImagesPath, imageShareFilePath);
                    // Resize it for front-end display
                    shareImage.Resize(1200, 1200);
                    shareImage.SaveJpg(destinationShareFilePath, 90, true);
                }
                */

                // Create the share image (wide aspect ratio)
                String backgroundShareImageWidePath = Path.Combine(cdmBarAssetsPath, "background-share-wide.png");
                using (var shareImageWide = new KalikoImage(backgroundShareImageWidePath))
                {
                    shareImageWide.SetResolution(96, 96);
                    shareImageWide.BlitImage(image, 1286, -68);
                    // Compose the text onto the image
                    String textShareImageWidePath = Path.Combine(cdmBarAssetsPath, "text-share-wide.png");
                    using (var shareTextWide = new KalikoImage(textShareImageWidePath))
                    {
                        shareTextWide.SetResolution(96, 96);
                        shareImageWide.BlitImage(shareTextWide);
                    }

                    // Save the wide share image                
                    String destinationShareWideFilePath = Path.Combine(userImagesPath, imageShareWideFilePath);
                    // Resize it for front-end display
                    shareImageWide.Resize(1200, 630);
                    shareImageWide.SaveJpg(destinationShareWideFilePath, 90, true);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Save the composited image
                String destinationFilePath = Path.Combine(userImagesPath, imageFilePath);
                image.SetResolution(96, 96);
                // Resize it for front-end display
                image.Resize(520, 360);
                image.SaveJpg(destinationFilePath, 90, true);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return new Dictionary<string, string> {
                { "FileId", fileId },
                { "CompositedImage", imageFilePath },
                { "CompositedImageShare", imageShareWideFilePath },
                { "CompositedImageShareWide", imageShareWideFilePath }
            };
        }

        public ResultDTO Process()
        {
            var participantId = Guid.NewGuid();

            // Captcha validation
            if (ConfigurationManager.AppSettings["GoogleRecaptcha:Enabled"] == "true")
            {
                var reCaptchaValidationModule = new ReCaptchaValidationModule()
                {
                    CaptchaSecret = ConfigurationManager.AppSettings["GoogleRecaptcha:SecretKey"],
                    CaptchaResponse = MediatorModel.ReCaptchaResponse
                };
                ResultDTO reCaptchaValidationResult = reCaptchaValidationModule.Process();
                if (reCaptchaValidationResult.HttpStatusCode != HttpStatusCode.OK)
                {
                    return reCaptchaValidationResult;
                }
            }

            // Background color validation
            var backgroundColorValidationModule = new BackgroundColorValidationModule()
            {
                BackgroundColor = MediatorModel.BarNameBackground
            };
            ResultDTO backgroundColorValidationResult = backgroundColorValidationModule.Process();
            if (backgroundColorValidationResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return backgroundColorValidationResult;
            }

            // Age format validation
            var dateFormatValidationModule = new DateFormatValidationModule()
            {
                DateString = MediatorModel.BirthDate
            };
            ResultDTO dateFormatValidationResult = dateFormatValidationModule.Process();
            if (dateFormatValidationResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return dateFormatValidationResult;
            }

            // Age check
            var birthDate = DateTime.ParseExact(MediatorModel.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var ageValidationModule = new AgeValidationModule()
            {
                BirthDate = birthDate,
                MaximumAge = 16
            };
            ResultDTO ageValidationResult = ageValidationModule.Process();
            if (ageValidationResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return ageValidationResult;
            }

            // Country check
            var countryCodeCheckModule = new CountryCodeCheckModule()
            {
                CountryCode = MediatorModel.CountryCode
            };
            ResultDTO countryCodeCheckResult = countryCodeCheckModule.Process();
            if (countryCodeCheckResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return countryCodeCheckResult;
            }

            // Ingredients validation
            var ingredientsValidationModule = new IngredientsValidationModule()
            {
                Ingredient1Raw = MediatorModel.Ingredient1,
                Ingredient2Raw = MediatorModel.Ingredient2,
                Ingredient3Raw = MediatorModel.Ingredient3
            };
            ResultDTO ingredientsValidationResult = ingredientsValidationModule.Process();
            if (ingredientsValidationResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return ingredientsValidationResult;
            }
            Ingredient ingredient1 = (Ingredient)ingredientsValidationResult.Meta.Ingredient1;
            Ingredient ingredient2 = (Ingredient)ingredientsValidationResult.Meta.Ingredient2;
            Ingredient ingredient3 = (Ingredient)ingredientsValidationResult.Meta.Ingredient3;

            // Get bar colour
            var barColourCheckModule = new BarColourCheckModule()
            {
                Ingredient1 = ingredient1,
                Ingredient2 = ingredient2,
                Ingredient3 = ingredient3
            };
            ResultDTO barColourCheckResult = barColourCheckModule.Process();

            // Initialize participant and data cache
            string emailHash = StringUtils.HashSHA256(MediatorModel.Email.ToLower());
            Participant participant = ParticipantManager.GetByEmailHash(emailHash);
            bool existingParticipant = true;
            if (participant == null) {
                participant = new Participant()
                {
                    Id = participantId,
                    EmailHash = emailHash,
                    ConsumerId = String.Empty,
                    CreatedOn = DateTime.UtcNow
                };
                existingParticipant = false;
            }

            ParticipantDataCache participantDataCache = new ParticipantDataCache()
            {
                Id = Guid.NewGuid(),
                FirstName = StringUtils.HashSHA256(MediatorModel.FirstName.ToLower()),
                LastName = StringUtils.HashSHA256(MediatorModel.LastName.ToLower()),
                Email = StringUtils.HashSHA256(MediatorModel.Email.ToLower()),
                MobileNumber = StringUtils.HashSHA256(MediatorModel.MobileNumber.ToLower()),
                CountryCode = MediatorModel.CountryCode,
                City = StringUtils.HashSHA256(MediatorModel.City.ToLower()),
                BirthDate = DateTime.ParseExact("1970-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                CreatedOn = DateTime.UtcNow
            };        

            // Build CDM Bar Image here   
            string tempUGIDirectory = ConfigurationManager.AppSettings["UGITempDirectory"];
            string destinationPath = Path.Combine(GetRequestApplicationPath(), tempUGIDirectory);
            Dictionary<string, string> images = BuildCDMBar(
                MediatorModel.BarName,
                MediatorModel.BarNameBackground,
                (ingredient1 == null) ? String.Empty : ingredient1.PackImagePath,
                (ingredient2 == null) ? String.Empty : ingredient2.PackImagePath,
                (ingredient3 == null) ? String.Empty : ingredient3.PackImagePath,
                participant.Id.ToString("N"),
                destinationPath
            );

            // Move the created entries to the storage account
            string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];            
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                // Successfully connected to storage account, let's instantiate a client that represents the Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Get a reference to the container
                string containerPath = ConfigurationManager.AppSettings["StorageContainerPath"];
                
                // Get base URI to the blob storage account
                Uri baseContainerUri = new Uri(ConfigurationManager.AppSettings["StorageUrl"]);                

                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerPath);
                //containerPath/ images["CompositedImage"]

                CloudBlockBlob cloudBlockBlob;
                // Upload composited image
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(images["CompositedImage"]);
                cloudBlockBlob.Properties.ContentType = "image/jpeg";
                cloudBlockBlob.UploadFromFile(Path.Combine(destinationPath, images["CompositedImage"]));
                //images["CompositedImage"] = cloudBlockBlob.Uri.ToString();
                images["CompositedImage"] = new Uri(baseContainerUri, $"{containerPath}/{images["CompositedImage"]}").ToString();

                // Upload composited share image
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(images["CompositedImageShare"]);
                cloudBlockBlob.Properties.ContentType = "image/jpeg";
                cloudBlockBlob.UploadFromFile(Path.Combine(destinationPath, images["CompositedImageShare"]));
                //images["CompositedImageShare"] = cloudBlockBlob.Uri.ToString();
                images["CompositedImageShare"] = new Uri(baseContainerUri, $"{containerPath}/{images["CompositedImageShare"]}").ToString();

                // Upload composited wide share image
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(images["CompositedImageShareWide"]);
                cloudBlockBlob.Properties.ContentType = "image/jpeg";
                cloudBlockBlob.UploadFromFile(Path.Combine(destinationPath, images["CompositedImageShareWide"]));
                //images["CompositedImageShareWide"] = cloudBlockBlob.Uri.ToString();
                images["CompositedImageShareWide"] = new Uri(baseContainerUri, $"{containerPath}/{images["CompositedImageShareWide"]}").ToString();

                // Generate HTML file for OG tags - useful for Twitter sharing
                string data = File.ReadAllText(Path.Combine(GetRequestApplicationPath(), "App_Data/share.html"))
                    .Replace("{{share_image}}", images["CompositedImageShareWide"])
                    .Replace("{{site_url}}", ConfigurationManager.AppSettings["SiteUrl"]);
                byte[] bytes = Encoding.ASCII.GetBytes(data);
                using (var htmlShareData = new MemoryStream(bytes))
                {
                    // Upload HTML file to storage
                    string htmlFilePath = $"{participant.Id.ToString("N")}/{images["FileId"]}.html";
                    cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(htmlFilePath);
                    cloudBlockBlob.Properties.ContentType = "text/html";
                    cloudBlockBlob.UploadFromStream(htmlShareData);
                    //images["ShareUrl"] = cloudBlockBlob.Uri.ToString();
                    images["ShareUrl"] = new Uri(baseContainerUri, $"{containerPath}/{htmlFilePath}").ToString();
                }
            }
            else
            {
                throw new Exception("Could not connect to Azure Cloud Storage");
            }


            // Initialize entry
            Entry entry = new Entry()
            {
                Id = Guid.NewGuid(),
                BarName = MediatorModel.BarName,
                BarDescription = MediatorModel.BarDescription,
                BarColour = MediatorModel.BarNameBackground,
                Ingredient1 = ingredient1,
                Ingredient2 = ingredient2,
                Ingredient3 = ingredient3,
                RejectedIngredients = MediatorModel.RejectedIngredients,
                CompositedImage = images["CompositedImage"],
                CompositedImageShare = images["CompositedImageShareWide"],
                Colour = barColourCheckResult.Meta.Colour,
                CreatedOn = DateTime.UtcNow
            };

            // Insert participant, data cache and entry
            if (!existingParticipant)
            {
                ParticipantManager.Insert(
                    participant,
                    participantDataCache,
                    entry
                );
            }
            else
            {
                ParticipantManager.InsertFromExistingParticipant(
                    participant,
                    participantDataCache,
                    entry
                );
            }

            // Send consultix transaction
            if (ConfigurationManager.AppSettings["ConsultixEnabled"] == "true")
            {
                var consultixParticipationModule = new ConsultixParticipationModule()
                {
                    CountryCode = MediatorModel.CountryCode.ToUpper(),
                    FirstName = MediatorModel.FirstName,
                    LastName = MediatorModel.LastName,
                    Email = MediatorModel.Email,
                    MobilePrivate = MediatorModel.MobileNumber,
                    City = MediatorModel.City,
                    Birthday = MediatorModel.BirthDate,
                    CadburyEmail = MediatorModel.EmailSubscription,
                    Ingredient1Category = (ingredient1 == null) ? String.Empty : ingredient1.Category,
                    Ingredient1Name = (ingredient1 == null) ? String.Empty : ingredient1.Name,
                    Ingredient1Colour = (ingredient1 == null) ? String.Empty : ingredient1.Colour,
                    Ingredient2Category = (ingredient2 == null) ? String.Empty : ingredient2.Category,
                    Ingredient2Name = (ingredient2 == null) ? String.Empty : ingredient2.Name,
                    Ingredient2Colour = (ingredient2 == null) ? String.Empty : ingredient2.Colour,
                    Ingredient3Category = (ingredient3 == null) ? String.Empty : ingredient3.Category,
                    Ingredient3Name = (ingredient3 == null) ? String.Empty : ingredient3.Name,
                    Ingredient3Colour = (ingredient3 == null) ? String.Empty : ingredient3.Colour,
                    BarColour = MediatorModel.BarNameBackground,
                    BarDescription = MediatorModel.BarDescription,
                    BarName = MediatorModel.BarName,
                    ParticipationId = participantId.ToString(),
                    EntryId = entry.Id
                };
                ResultDTO consultixParticipationResult = consultixParticipationModule.Process();
                if (consultixParticipationResult.HttpStatusCode == HttpStatusCode.OK)
                {
                    participant.ConsumerId = consultixParticipationResult.Meta.ConsumerId;
                    ParticipantManager.Update(participant);
                }
            }

            Result.HttpStatusCode = HttpStatusCode.OK;
            Result.Meta = new
            {
                pid = participantId.ToString("N"),
                eid = entry.Id.ToString("N"),
                cid = !String.IsNullOrEmpty(participant.ConsumerId.Trim()) ? Guid.Parse(participant.ConsumerId).ToString("N") : "unknown_"+entry.Id.ToString("N"),
                BarImage = images["CompositedImage"],
                ShareImage = images["CompositedImageShare"],
                ShareUrl = images["ShareUrl"]
            };

            return Result;
        }
    }
}
