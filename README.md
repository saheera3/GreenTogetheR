GreenTogether – Waste Collection & Recycling Management System

Overview

GreenTogether is a web-based waste collection and recycling management system designed to encourage responsible waste disposal and community participation through digital reporting, awareness, and incentive mechanisms.

The idea, system design, and implementation were fully conceptualized and developed independently to address real-world environmental challenges such as illegal dumping, lack of recycling awareness, and poor citizen–authority coordination.

 Problem Statement

Urban waste management faces recurring issues including:
	•	Illegal dumping in public and residential areas
	•	Low public engagement in recycling initiatives
	•	Limited visibility of recycling facilities and collection schedules
	•	Absence of incentive systems to motivate responsible behavior

GreenTogether aims to bridge this gap by combining reporting, education, and rewards within a single digital platform.

System Objectives
	•	Enable citizens to report illegal dumping incidents digitally
	•	Improve public awareness of recycling and waste management
	•	Provide easy access to recycling centers and collection schedules
	•	Encourage eco-friendly behavior through a points-based reward system
	•	Support administrative monitoring and data-driven decision making

⸻

 Core Features

User Features
	•	Secure user registration and login
	•	Illegal dumping report submission
	•	Recycling center locator
	•	Waste collection schedule viewing
	•	Awareness and educational content access
	•	Points accumulation for eco-friendly activities
	•	User status levels based on points:
	•	Member (0–49)
	•	Eco Helper (50–99)
	•	Green Warrior (100+)



 Admin Features
	•	Manage awareness articles (CRUD)
	•	Manage recycling center information
	•	Manage waste collection schedules
	•	View all illegal dumping reports
	•	Export dumping reports as PDF for documentation and analysis


 Technologies Used
	•	Backend: ASP.NET Core MVC
	•	Language: C#
	•	Database: Microsoft SQL Server
	•	Frontend: HTML, CSS, JavaScript, Bootstrap
	•	Development Tools: Visual Studio, SQL Server Management Studio (SSMS)



Database Design

The system uses a relational database with normalized tables to ensure data integrity and scalability.

Key entities include:
  Cities
	Users
	Illegal Dumping Reports
	Recycling Centers
	Waste Schedules
	Awareness Articles
	User Points and Status Levels

Primary keys use IDENTITY columns to maintain consistency and uniqueness.

 System Architecture
	•	MVC (Model–View–Controller) architecture
	•	Separation of concerns between presentation, logic, and data layers
	•	Secure authentication and role-based access handling
	•	Modular design for future scalability



How to Run the Project
	1.	Clone the repository
	2.	Open the solution in Visual Studio
	3.	Restore NuGet packages
	4.	Configure the SQL Server connection string in appsettings.json
	5.	Run database scripts to create required tables
	6.	Build and run the application



 Impact & Significance

GreenTogether promotes:
	•	Environmental responsibility
	•	Community engagement
	•	Digital transformation in public waste management

The system demonstrates how technology can be leveraged to address sustainability challenges through practical, user-centered solutions.


Future Enhancements
	•	Mobile application integration
	•	Image-based dumping verification
	•	Data analytics dashboards for authorities
	•	Multilingual support
	•	Automated waste collection notifications

## Project Type
Academic Project – Completed
