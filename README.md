# Email Generator Library 

## Overview

This C# library provides a simple way to generate Outlook-compatible `.msg` email files. The library handles email composition, validation, and file generation with support for attachments, CC/BCC recipients, and custom formatting.
You can also find a .NET c# console application (EmailDllTest) as well to test the library.

## Features

- Generate professional emails with UN/DESA signature and formatting
- Support for attachments, CC/BCC recipients
- Automatic validation of email addresses and content
- Thread-safe operations with both synchronous and asynchronous methods
- Customizable output directory for generated `.msg` files

## Prerequisites

- .NET Framework 4.6.1 or later
- [MsgKit NuGet package](https://www.nuget.org/packages/MsgKit/) (required for `.msg` file generation)

## Installation

1. Add the compiled DLL to your project references
2. Install the MsgKit NuGet package:
   ```
   Install-Package MsgKit
   ```

## Quick Start

```csharp
// Initialize the email service
var emailService = new EmailService();

// Generate a basic email
string filePath = emailService.GenerateEmail(
    receiverEmail: "recipient@example.com",
    receiverName: "John Doe",
    subject: "Test Email",
    body: "This is a test email body.",
    receiverTitle: "Mr" // Optional title (Mr, Ms, Dr, etc.)
);

Console.WriteLine($"Email generated at: {filePath}");
```

## Core Usage

### 1. Basic Email Generation

```csharp
var emailService = new EmailService();

string filePath = emailService.GenerateEmail(
    receiverEmail: "recipient@example.com",
    receiverName: "Recipient Name",
    subject: "Email Subject",
    body: "Email body content",
    receiverTitle: "Dr" // Optional
);
```

### 2. Email with CC/BCC and Attachments

```csharp
var emailService = new EmailService();

var ccRecipients = new List<string> { "cc1@example.com", "cc2@example.com" };
var bccRecipients = new List<string> { "bcc@example.com" };
var attachments = new List<string> { @"C:\path\to\file.pdf" };

string filePath = emailService.GenerateEmail(
    receiverEmail: "recipient@example.com",
    receiverName: "Recipient Name",
    subject: "Email with Attachments",
    body: "Please see attached files.",
    receiverTitle: "Ms",
    ccRecipients: ccRecipients,
    bccRecipients: bccRecipients,
    attachmentPaths: attachments
);
```

### 3. Using EmailData Directly (Advanced)

```csharp
var emailData = new EmailData();
emailData.ReceiverEmail = "recipient@example.com";
emailData.ReceiverName = "Recipient Name";
emailData.Subject = "Direct EmailData Test";
emailData.Body = "This email was created using EmailData directly.";

// Add CC/BCC
emailData.AddCcRecipient("cc@example.com");
emailData.AddBccRecipient("bcc@example.com");

// Add attachment
emailData.TryAddAttachment(@"C:\path\to\file.pdf");

// Generate email
var emailGenerator = new EmailGenerator(outputDirectory);
string filePath = emailGenerator.GenerateEmail(emailData);
```

## Validation Methods

```csharp
var emailService = new EmailService();

// Validate email address
bool isValidEmail = emailService.ValidateEmail("test@example.com");

// Validate subject
bool isValidSubject = emailService.ValidateSubject("Valid Subject");

// Validate attachment
var fileInfo = emailService.ValidateAttachment(@"C:\path\to\file.pdf");

// Get available title prefixes
string[] titles = emailService.GetAvailableTitlePrefixes();
```

## Configuration

```csharp
// Change output directory
var emailService = new EmailService();
emailService.SetOutputDirectory(@"C:\New\Output\Directory");

// Get current output directory
string currentDir = emailService.OutputDirectory;
```

## Notes

- The library automatically adds the UN/DESA signature to all generated emails
- Generated `.msg` files can be opened directly in Microsoft Outlook
- All methods have both synchronous and asynchronous versions
- Comprehensive input validation is performed automatically

## Example Output

Generated emails will include:
```
Dear [Title] [Name],

[Your message body]

Best regards,

UN|DESA Office
United Nations Department of Economic and Social Affairs
c/o FAO HQs, Rooms: B001 - B006
Via delle Terme di Caracalla - 00153 Rome – Italy
Tel: (+39) 06-5705 4638
```
#####################################################
