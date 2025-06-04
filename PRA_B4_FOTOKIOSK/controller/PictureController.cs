using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Huidige dagnummer ophalen (0 = zondag, 6 = zaterdag)
            int today = (int)DateTime.Now.DayOfWeek;

            // Pad naar de foto's map
            string basePath = @"../../../fotos";

            // Doorloop alle dag-mappen (bijv. 0_Zondag, 1_Maandag, ...)
            foreach (string dir in Directory.GetDirectories(basePath))
            {
                // Mapnaam ophalen, bijvoorbeeld "2_Dinsdag"
                string dirName = Path.GetFileName(dir);

                // Probeer het dagnummer (voor de "_") uit te lezen
                string[] parts = dirName.Split('_');
                if (parts.Length > 0 && int.TryParse(parts[0], out int dirDayNumber))
                {
                    // Alleen de map van vandaag gebruiken
                    if (dirDayNumber == today)
                    {
                        // Voeg alle foto's toe uit deze map
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            PicturesToDisplay.Add(new KioskPhoto()
                            {
                                Id = 0,
                                Source = file
                            });
                        }
                    }
                }
            }

            // Update de foto's op het scherm
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Leeg de huidige lijst
            PicturesToDisplay.Clear();
            // Herstart de filtering
            Start();
        }
    }
}
