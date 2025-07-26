using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EmailGeneratorLibrary;

namespace EmailDllTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Email Generator Library Test");
            Console.WriteLine("===========================");

            try
            {
                // Test 1: Basic email generation using EmailService
                TestBasicEmailGeneration();

                // Test 2: Email with attachments and CC/BCC
                TestAdvancedEmailGeneration();

                // Test 3: Direct EmailData usage
                TestDirectEmailDataUsage();

                // Test 4: Validation methods
                TestValidationMethods();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during testing: {ex.Message}");
                Console.WriteLine($"Details: {ex}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void TestBasicEmailGeneration()
        {
            Console.WriteLine("\n1. Testing Basic Email Generation");
            Console.WriteLine("---------------------------------");

            try
            {
                // Create EmailService with default output directory
                //var emailService = new EmailService();
                var emailService = new EmailService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GeneratedEmails"));

                // Generate a simple email
                string filePath = emailService.GenerateEmail(
                    receiverEmail: "john.doe@example.com",
                    receiverName: "John Doe",
                    subject: "Test Email from Library",
                    body: "This is a test email generated using the EmailGeneratorLibrary.",
                    receiverTitle: "Mr"
                );

                Console.WriteLine($"✓ Basic email generated successfully!");
                Console.WriteLine($"  File saved to: {filePath}");
                Console.WriteLine($"  File exists: {File.Exists(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Basic email generation failed: {ex.Message}");
            }
        }

        static void TestAdvancedEmailGeneration()
        {
            Console.WriteLine("\n2. Testing Advanced Email Generation");
            Console.WriteLine("------------------------------------");

            try
            {
                var emailService = new EmailService();

                // Create lists for CC and BCC recipients
                var ccRecipients = new List<string> { "cc1@example.com", "cc2@example.com" };
                var bccRecipients = new List<string> { "bcc1@example.com" };

                // Note: For testing, we won't add actual attachments since we don't have files
                // In real usage, you would add actual file paths like:
                // var attachments = new List<string> { @"C:\Users\fabio\OneDrive\Documents\cv-ita-02.pdf" };

                string filePath = emailService.GenerateEmail(
                    receiverEmail: "jane.smith@example.com",
                    receiverName: "Jane Smith",
                    subject: "Advanced Test Email with CC/BCC",
                    body: "This email demonstrates CC, BCC, and title functionality.",
                    receiverTitle: "Dr",
                    ccRecipients: ccRecipients,
                    bccRecipients: bccRecipients
                //   attachmentPaths: attachments
                );

                Console.WriteLine($"✓ Advanced email generated successfully!");
                Console.WriteLine($"  File saved to: {filePath}");
                Console.WriteLine($"  CC Recipients: {string.Join(", ", ccRecipients)}");
                Console.WriteLine($"  BCC Recipients: {string.Join(", ", bccRecipients)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Advanced email generation failed: {ex.Message}");
            }
        }

        static void TestDirectEmailDataUsage()
        {
            Console.WriteLine("\n3. Testing Direct EmailData Usage");
            Console.WriteLine("---------------------------------");

            try
            {
                // Create EmailData directly
                var emailData = new EmailData();
                emailData.ReceiverEmail = "direct@example.com";
                emailData.ReceiverName = "Direct User";
                emailData.ReceiverTitle = "Ms";
                emailData.Subject = "Direct EmailData Test";
                emailData.Body = "This email was created using EmailData directly.";

                // Add CC and BCC recipients
                emailData.AddCcRecipient("cc.direct@example.com");
                emailData.AddBccRecipient("bcc.direct@example.com");

                // Validate the email data
                bool isValid = emailData.IsValid();
                Console.WriteLine($"  EmailData is valid: {isValid}");

                if (isValid)
                {
                    // Generate the email using EmailGenerator directly
                    var emailGenerator = new EmailGenerator(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GeneratedEmails"));
                    string filePath = emailGenerator.GenerateEmail(emailData);

                    Console.WriteLine($"✓ Direct EmailData email generated successfully!");
                    Console.WriteLine($"  File saved to: {filePath}");
                    Console.WriteLine($"  Generated filename: {emailData.GenerateFileName()}");
                }
                else
                {
                    Console.WriteLine("✗ EmailData validation failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Direct EmailData usage failed: {ex.Message}");
            }
        }

        static void TestValidationMethods()
        {
            Console.WriteLine("\n4. Testing Validation Methods");
            Console.WriteLine("-----------------------------");

            var emailService = new EmailService();

            // Test email validation
            string[] testEmails = {
                "valid@example.com",
                "invalid.email",
                "another@valid.co.uk",
                "@invalid.com",
                "valid.email+tag@example.org"
            };

            Console.WriteLine("  Email Validation Results:");
            foreach (string email in testEmails)
            {
                bool isValid = emailService.ValidateEmail(email);
                Console.WriteLine($"    {email}: {(isValid ? "✓ Valid" : "✗ Invalid")}");
            }

            // Test subject validation
            string[] testSubjects = {
                "Valid Subject",
                "AB", // Too short
                "This is a very long subject that might exceed the maximum length limit for email subjects which is typically 255 characters but we're testing to make sure our validation catches subjects that are way too long and prevents them from being processed which could cause issues", // Too long
                "",   // Empty
                "Perfect Length Subject"
            };

            Console.WriteLine("\n  Subject Validation Results:");
            foreach (string subject in testSubjects)
            {
                bool isValid = emailService.ValidateSubject(subject);
                string displaySubject = subject.Length > 50 ? subject.Substring(0, 50) + "..." : subject;
                Console.WriteLine($"    \"{displaySubject}\": {(isValid ? "✓ Valid" : "✗ Invalid")}");
            }

            // Test available title prefixes
            string[] titles = emailService.GetAvailableTitlePrefixes();
            Console.WriteLine($"\n  Available Title Prefixes: {string.Join(", ", titles)}");
        }
    }


}

