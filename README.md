# VirtualEvent
Virtual Event Scheduler & Management Platform

A complete event management solution with WinForms Desktop App, ASP.NET Web Portal, and .NET Core Web API. Designed for admins, staff, and attendees to manage and participate in virtual events.

# What’s Included
✅ WinForms Desktop App (Admin & Staff Panel)
✅ ASP.NET Web Interface (Attendee Portal)
✅ ASP.NET Core Web API (Backend)

# Desktop Application – Admin & Staff Control Panel
- Accessible only by admins and staff, the desktop application is designed for secure, role-based event management.

- Features:

   1. Create, edit, and delete events

   1. Assign event details (title, time, description, capacity, etc.)

   1. View registered participants for each event

   1. Manage all events through a structured interface

- Restricted access for admins and staff only
  
![image](https://github.com/user-attachments/assets/184139d9-4174-451c-9905-775f46fb7188)

![image](https://github.com/user-attachments/assets/9f636b5c-683a-4338-bb2e-bb7a523ace0f)

![image](https://github.com/user-attachments/assets/30b9a0e0-45f7-4010-9a14-f1d8752b9e86)


# Web Interface – Public Event Portal

- Accessible by all users, including attendees, through a browser.

- Features:

   1. User registration and login

   1. Browse upcoming and past events

   1. View detailed event information

   1. Register or unregister for events

   1. See a list of all attended events

   1. Filter events by status, date, and number of participants

   1. Receive real-time event updates and change notifications
 
![image](https://github.com/user-attachments/assets/7eb76fa1-7517-4c4b-acd0-2c3d8ff12383)  ![image](https://github.com/user-attachments/assets/9ffd6804-8ba2-4c3b-b090-8a23dbbc62cd)

![image](https://github.com/user-attachments/assets/479f8f08-3132-4d08-be13-48e44ee27489)

![image](https://github.com/user-attachments/assets/e8d51c5a-159c-436c-ad47-74b4011cef57)

![image](https://github.com/user-attachments/assets/a63854f0-d745-4def-8022-3c08d97d5d66)

# Functional Requirements – All Met
Every item from the Functional Requirements list has been successfully implemented:

✔️ Secure login with username/password
✔️ Role-based access (admin/staff vs attendee)
✔️ Admin/staff-only desktop access
✔️ Full event management functionality
✔️ Filtering and listing for all users
✔️ Participant list view (admin/staff only)
✔️ Registration and unregistration logic
✔️ One-event-per-timeline limit for attendees
✔️ Attendance history
✔️ Meeting updates and in-app notifications

# How to Run This Project

1️⃣ Download the Release

🔗 Source code (zip)

📁 Extract the .zip file

2️⃣ Open the Solution

📂 Open VirtualEvent_SE410.sln in Visual Studio 2022+

3️⃣ Start All Projects Together

1. Right-click the solution → Set Startup Projects

1. Choose Multiple startup projects

1. Set these to Start:

   - VirtualEvent_SE410 (Desktop App for admin/staff)

   - WEBAPI (ASP.NET Core API)

   - VirtualEventWEB (ASP.NET Web Interface)


![image](https://github.com/user-attachments/assets/20ba4320-5d20-4a84-9bbb-b7a769ec0350)


4. Press Ctrl + F5

🖥 The desktop app launches for admin/staff

🌐 Web API on https://localhost:44393/

🌐 Web portal on https://localhost:44394/
