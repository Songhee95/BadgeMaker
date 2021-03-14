using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Net;

namespace CatWorx.BadgeMaker
{
    class Util
    {
        //make the methods on Util public
        public static void PrintEmployees(List<Employee> employeesFromMain)
        {
            for (int i = 0; i < employeesFromMain.Count; i++)
            {
                // print out all the employee information for each employee
                string template = "{0,-10}\t{1,-20}\t{2}";
                Console.WriteLine(String.Format(
                    template,
                    employeesFromMain[i].GetId(),
                    employeesFromMain[i].GetName(),
                    employeesFromMain[i].GetPhotoUrl()
                    ));
            }
        }
        //Add another static method to make CSV file.
        public static void MakeCSV(List<Employee> employeesFromMain)
        {
            // Check to see if folder exists
            if (!Directory.Exists("data"))
            {
                //If not, create it
                Directory.CreateDirectory("data");
            }
            // StreamWriter file = new StreamWriter("data/employees.csv");
            // We don't want potentially heavy resources to continue to hog up memory after we're done using them.
            // We could manually dispose the StreamWriter ourselves once we're done with it 
            using (StreamWriter file = new StreamWriter("data/employees.csv"))
            {
                file.WriteLine("ID, Name, PhotoUrl");
                for (int i = 0; i < employeesFromMain.Count; i++)
                {
                    string template = "{0},{1},{2}";
                    file.WriteLine(
                        String.Format(
                            template,
                            employeesFromMain[i].GetId(),
                            employeesFromMain[i].GetName(),
                            employeesFromMain[i].GetPhotoUrl()
                        )
                    );
                }
            }
        }

        public static void MakeBadges(List<Employee> employeesFromMain)
        {
            // Layout variables
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;

            int COMPANY_NAME_START_X = 0;
            int COMPANY_NAME_START_Y = 110;
            int COMPANY_NAME_WIDTH = 100;

            int PHOTO_START_X = 184;
            int PHOTO_START_Y = 215;
            int PHOTO_WIDTH = 302;
            int PHOTO_HEIGHT = 302;

            int EMPLOYEE_NAME_START_X = 0;
            int EMPLOYEE_NAME_START_Y = 560;
            int EMPLOYEE_NAME_WIDTH = BADGE_WIDTH;
            int EMPLOYEE_NAME_HEIGHT = 100;

            int EMPLOYEE_ID_START_X = 0;
            int EMPLOYEE_ID_START_Y = 690;
            int EMPLOYEE_ID_WIDTH = BADGE_WIDTH;
            int EMPLOYEE_ID_HEIGHT = 100;

            // Graphics objects
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            int FONT_SIZE = 32;
            Font font = new Font("Arial", FONT_SIZE);
            Font monoFont = new Font("Courier New", FONT_SIZE);

            SolidBrush brush = new SolidBrush(Color.Black);

            //instance of WebClient is disposed after code in the black has run
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < employeesFromMain.Count; i++)
                {
                    // Convert photo URL into a stream by using OpenRead() method
                    Stream employeeStream = client.OpenRead(employeesFromMain[i].GetPhotoUrl());
                    // Convert the Stream into an Image using Image.FromStream() method
                    Image photo = Image.FromStream(employeeStream);
                    // Create the canvas to create our badge by using the handy size constants
                    Image background = Image.FromFile("badge.png");
                    Image badge = new Bitmap(BADGE_WIDTH, BADGE_HEIGHT);
                    Graphics graphic = Graphics.FromImage(badge);
                    graphic.DrawImage(background, new Rectangle(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
                    graphic.DrawImage(photo, new Rectangle(PHOTO_START_X, PHOTO_START_Y, PHOTO_WIDTH, PHOTO_HEIGHT));
                    // Company name display
                    graphic.DrawString(
                        employeesFromMain[i].GetCompanyName(),
                        font,
                        new SolidBrush(Color.White),
                        new Rectangle(
                            COMPANY_NAME_START_X,
                            COMPANY_NAME_START_Y,
                            BADGE_WIDTH,
                            COMPANY_NAME_WIDTH
                        ),
                        format
                    );
                    // Employee name display
                    graphic.DrawString(
                        employeesFromMain[i].GetName(),
                        font,
                        new SolidBrush(Color.White),
                        new Rectangle(
                            EMPLOYEE_NAME_START_X,
                            EMPLOYEE_NAME_START_Y,
                            BADGE_WIDTH,
                            EMPLOYEE_NAME_HEIGHT
                        ),
                        format
                    );
                    // Employee Id display
                    graphic.DrawString(
                        employeesFromMain[i].GetId().ToString(),
                        monoFont,
                        brush,
                        new Rectangle(
                            EMPLOYEE_ID_START_X,
                            EMPLOYEE_ID_START_Y,
                            EMPLOYEE_ID_WIDTH,
                            EMPLOYEE_ID_HEIGHT
                        ),
                        format
                    );

                    string template = "data/{0}_badge.png";
                    badge.Save(string.Format(template, employeesFromMain[i].GetId()));
                }
            }
        }
    }
}

