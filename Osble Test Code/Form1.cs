using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;
using System.IO;
using Osble_Test_Code.Properties;
using Osble_Test_Code.OsbleServiceReference;
using Osble_Test_Code.ServiceReference1;
using Osble_Test_Code.ServiceReference2;
using Osble_Test_Code.Uploader;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Osble_Test_Code
{
    public partial class wSignIn : Form

    {


        public wSignIn()
        {
            InitializeComponent();
            usrTextBox.Text = "brian.fleck@email.wsu.edu";
            pwdTextBox.Text = "824629";
        }

        private void usrLabel_Click(object sender, EventArgs e)
        {

        }

        private void pwdLabel_Click(object sender, EventArgs e)
        {

        }

        private void signButton_Click(object sender, EventArgs e)
        {
            if (pwdLabel.Text != null && userLabel.Text != null)
            {
               
                try{

                    AuthenticationServiceClient authent = new AuthenticationServiceClient();

                    OsbleServiceClient client = new OsbleServiceClient();

                      
                    authent.Open();
                    

                     String response = authent.ValidateUser(usrTextBox.Text, pwdTextBox.Text);

                     OsbleServiceReference.UserProfile profile = authent.GetActiveUser(response);

                     Course[] userCourse = client.GetCourses(response);

                     resLabel.Text = "";

                     UploaderWebServiceClient test = new UploaderWebServiceClient();

                   

                     Assignment[] assignments = client.GetCourseAssignments(userCourse[2].ID, response);

                     bool temp = test.DeleteFile("Assignment1.pdf", userCourse[2].ID, response);

                     resLabel.Text += "-" + userCourse[2].Name + "\n";

                     for (int j = 0; j < assignments.Length; j++)
                     {
                        // resLabel.Text += " - ID: " +assignments[j].ID +  " " + assignments[j].AssignmentName
                        //     + " due at " + assignments[j].DueTime.ToString()
                        //     + "\n";
                     }


                   // resLabel.Text += "Attempting File Submission..\n"; 
                    
                    //Assignmnet 1 Submission Attempt
                    ZipFile file = new ZipFile();
                    FileStream stream = File.OpenRead("C:\\Projects\\Osble Test Code\\Test Files\\Test.pdf");
                    MemoryStream A1Strm = new MemoryStream();
                    file.Save(A1Strm);                   
                    file.AddEntry("Assignments12.pdf", stream);
                    bool accepted =  client.SubmitAssignment(assignments[1].ID, A1Strm.ToArray(), response);
                    //Assert.AreEqual(true, accepted);
                    file.RemoveEntry("Assignments12.pdf");
                    stream.Close();
                    A1Strm.Close();
                   
                    //Assignment 2 Submission Attempt
                    ZipFile file1 = new ZipFile();
                    FileStream stream1 = File.OpenRead("C:\\Projects\\Osble Test Code\\Test Files\\Test1.pdf");
                    MemoryStream A2Strm = new MemoryStream();
          
                    file1.AddEntry("Assignments22.pdf", stream1);
                    file1.Save(A2Strm);
                    bool accepted1 = client.SubmitAssignment(assignments[2].ID, A2Strm.ToArray(), response);
                   // Assert.AreEqual(true, accepted1);
                    file1.RemoveEntry("Assignments22.pdf");
                    stream1.Close();
                    A2Strm.Close();

                    //Past Due Submission Attempt
                    ZipFile file2 = new ZipFile();

                    FileStream stream2 = File.OpenRead("C:\\Projects\\Osble Test Code\\Test Files\\Test2.pdf");
                    file2.AddEntry("PastDue1.pdf", stream2);
                    MemoryStream pastStrm = new MemoryStream();
                    file2.Save(pastStrm);
                    bool accepted2 = client.SubmitAssignment(assignments[0].ID, pastStrm.ToArray(), response);
                  //  Assert.AreEqual(false, accepted2);
                    file2.RemoveEntry("PastDue1.pdf");
                    stream2.Close();
                    pastStrm.Close();
           

                    for (int i = 0; i < 3; i++)
                    {
                        
                        byte[] subs = client.GetAssignmentSubmission(assignments[i].ID, response);
                        using (ZipFile reader = ZipFile.Read(subs))
                        {
                            foreach(ZipEntry x in reader)
                            {
                                //resLabel.Text += x.FileName + "\n";
                               // Assert.AreEqual(1, reader.Entries.Count);
                            }
                        }

                        
                    }
                    
                    resLabel.Text += "\n";
                   
                    //Time test code here checks due times agains the local time of the system.
                    foreach (Assignment a in assignments)
                    {
                       resLabel.Text += a.AssignmentName + ":\n";
                       resLabel.Text += "Assignment due Time obtained directly from OSBLE: " + a.DueDate + "\n" + a.DueTime + "\n";
                       resLabel.Text += "Assignment due Time in PST: " + a.DueDate.ToLocalTime() + "\n" + a.DueTime.ToLocalTime() + "\n";
                       resLabel.Text += "Dates are the same: " + (a.DueDate == a.DueDate.ToLocalTime()).ToString() + "\n";
                       resLabel.Text += "Times are the same: " + (a.DueTime == a.DueTime.ToLocalTime()).ToString() + "\n\n";

                        break;
                    }


                    //Gradebook upload test
                    try
                    {
                        int uploadGB = client.UploadCourseGradebook(userCourse[2].ID, pastStrm.ToArray(), response);

                        //Should not be allowed to upload a gradebook
                        Assert.AreEqual(-1, uploadGB);
                        resLabel.Text += "Grade Book: User couldn't upload a gradebook.\n";
                    }
                    catch (AssertFailedException n)
                    {
                        Console.WriteLine(n.ToString());

                        resLabel.Text += "Grade Book: User could upload a gradebook.\n";
                    }


                    UploaderWebServiceClient uploader = new UploaderWebServiceClient();
                    AssignmentSubmissionServiceClient subClient = new AssignmentSubmissionServiceClient();

                    //Delete File test

                        String[] filename = new String[4];
                        filename[0] = "ice_caves_in_iceland.jpg";
                        filename[1] = "Lava-flows-hawaii.png";
                        filename[2] = "TestfilefromEO.txt";
                        filename[3] = "Tyrol-Austria-austria-31748795-1600-1200.jpg";
                        //Should not be allowed to delete a file
                        for(int k = 0; k < 4; k++)
                        {
                            try
                            {
                                Assert.AreEqual(false, uploader.DeleteFile(filename[k], userCourse[2].ID, response));
                                resLabel.Text += "Files Deletion: User was unable to delete " + filename[k] + ".\n";

                            }catch(AssertFailedException n)
                            {
                                resLabel.Text += "Files Deletion: User was able to delete " + filename[k] + ".\n";
                            }
                        }
                    

                    String res2 = uploader.ValidateUser(usrTextBox.Text, pwdTextBox.Text);
                    //Activity Message Test
                    try
                    {
                        Assert.AreEqual(true, uploader.PostActivityMessage("This is a Test.\n", userCourse[2].ID, res2));
                        resLabel.Text += "Activity Message: User was able to post an activity message.\n";
                       
                    }
                    catch (AssertFailedException v)
                    {
                        Console.WriteLine(v.ToString());
                        resLabel.Text += "Activity Message: User was unable to Post an Activity Message.\n";
                    }

                    
                        foreach (Assignment x in assignments)
                        {
                            try
                            {
                                if (x.AssignmentName == "Critical review assignment #1")
                                {
                                    Assert.AreEqual(true, client.SubmitReview(profile.ID, x.ID, pastStrm.ToArray(), response));
                                     resLabel.Text += "Review Assignment: " + x.AssignmentName + " accepted the review when it should\n";
                                }
                                else
                                {
                                    Assert.AreEqual(false, client.SubmitReview(profile.ID, x.ID, pastStrm.ToArray(), response));
                                    resLabel.Text += "Review Assignment: " + x.AssignmentName + " failed when it should\n";
                                }
                            }
                            catch (AssertFailedException m)
                            {

                                if (x.AssignmentName == "Critical review assignment #1")
                                {
                                    resLabel.Text += "Review Assignment: " + x.AssignmentName + " failed to accept the review when it should\n";
                                }
                                else
                                {
                                    resLabel.Text += "Review Assignment: " + x.AssignmentName + " accepted the review when it should not have\n";
                                }
                            }
                        }

                        //Get course role
                        CourseRole temp2 = client.GetCourseRole(userCourse[0].ID, response);
                       
                        try
                        {

                            Assert.AreEqual("Student", temp2.Name);
                            resLabel.Text += "User is a student,";
                            Assert.AreEqual(false, temp2.CanGrade);
                            resLabel.Text += " who can't grade assignments,";
                            Assert.AreEqual(false, temp2.CanModify);
                            resLabel.Text += " who can't modify the course,";
                            Assert.AreEqual(false, temp2.CanSeeAll);
                            resLabel.Text += " who can't see hidden infromation,";
                            Assert.AreEqual(true, temp2.CanSubmit);
                            resLabel.Text += " who can submit assignments,";
                            Assert.AreEqual(false, temp2.CanUploadFiles);
                            resLabel.Text += " and who can't upload files.\n";
                        }
                        catch (AssertFailedException l)
                        {
                            resLabel.Text += " next test failed\n";
                        }

                        
                    //Get assignment test
                    byte[] dataArray = client.GetAssignmentSubmission(assignments[1].ID, response);

                    using (ZipFile testZip = ZipFile.Read(dataArray))
                    {
                        try
                        {
                            Assert.AreEqual(2, testZip.Entries.Count);
                            resLabel.Text += "two (2) entry found.\n";
                        }
                        catch (AssertFailedException g)
                        {
                            resLabel.Text += "Submission cound didn't equal two (2).\n";
                        }

                    }

                    authent.Close();
                   
                    //for(int i = 0; i < userCourse.Length; i++)
                    //{

                    //    Assignment[] assignments = client.GetCourseAssignments(userCourse[i].ID, response);
                    //    resLabel.Text += "-" + userCourse[i].Name + "\n";

                    //    for (int j = 0; j < assignments.Length; j++)
                    //    {
                    //        resLabel.Text += " -" +assignments[j].AssignmentName
                    //            + " due at " + assignments[j].DueTime.ToString()
                    //            + "\n";
                    //    }

                    //}

                                     
                    //if (response != "")
                    //{
                    //    resLabel.Text += response + "\nEnd of Response";
                    // }
                    // else
                    // {
                    //       resLabel.Text += "No message returned\n";
                    // }


                  
                   
                    
                 }catch(Exception error){
                     Console.WriteLine(error.ToString());
                }
            }
        }
    }
}
