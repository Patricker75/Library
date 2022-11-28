# Library
Updated as of 11/27/2022 9:59 pm CST.

Endless Knowledge is a library database being run using Azure Data Studio and
Visual Studio 2022.

Here at Endless Knowledge, any member can take advantage of our different utilities. They can check out not only books but devices like e-Readers and laptops too.
You can check out up to 3 books at a time (not counting device). We also offer a wide range of services you can use including our 3D Printing Lab, DNA Lab, and we
even have Indoor Go-Karts. There is something for members of all ages at our library.


Steps to Install Project onto Azure Data Studio
------------------------------------------------
 1) On your browser, go to https://learn.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver16
 2) If you already have Azure Data Studio installed, move on to step 6.
 3) Select the installer that corresponds to your operating system (Windows, macOS, Linux).
 4) Open the installer and read License Agreement before continuing.
 5) Follow the instructions for installation. Don't check or uncheck anything; keep clicking the "Next" button. We will use the default setup for this project.
 6) Once installation is complete, launch Azure Data Studio.
 7) On the uppermost lefthand corner, there is a button labelled "Connections". Click on that.
 8) Click on the little icon that's labelled "New Connection".
 9) Under Connection Details, paste the following link into the Server box: endlessknowledge.database.windows.net
10) For Authentication type, change it from Windows Authentication to SQL Login.
11) Enter the username and password of the member logging in. We will use the login of the team leader for this scenario.
    Username: Azureuser
    Password: libraryG17
12) You should see two databases listed on the page: Library and master.



Steps to Install Project onto Visual Studio 2022
------------------------------------------------
 1) On your browser, go to https://visualstudio.microsoft.com/downloads/
 2) Select "Free Download" under the Community header; this is the version we will use.
 3) Open the installer. On the Workloads page, checkmark "ASP.NET and web development". ASP.NET will be used when building web applications for the library database.
 4) Click install. This will take a while to finish downloading.
 5) Once installation is complete, launch Visual Studio 2022.
 6) On the righthand side, click "Clone a respository".
 7) Under Repository location, paste the following link: https://github.com/Patricker75/Library.git
 8) Click "Clone" to begin cloning repository.



User Roles & Their Login Credentials
------------------------------------
Member
    - Username: demami
    - Password: mew314tex
Librarian
    - Username: testLib
    - Password: passtest
Technician
    - Username: testTech
    - Password: passtest
Admin
    - Username: admin
    - Password: pass



How To Navigate Library Database Website
-----------------------------------------
Web App Site: https://endlessknowledge.azurewebsites.net/

Disclaimer: All of this is done from the main page of Endless Knowledge.


User Not Logged In
------------------
View books in catalog: Book Catalog
View rooms: Library Room
View devices: Device Catalog
View services: Offered Services
View resources: Library Resources


Member
------
Check out book: Book Catalog -> Title of Book -> Check Out
Check in book: Member Name (i.e. Daniel Emami) -> Books Checked Out -> Return
Place hold on book: Book Catalog -> Title of Book (where quantity is 0) -> Hold
Check out device: Device Catalog -> Device Name -> Check Out
Check in device: Member Name -> Devices Checked Out -> Return
Use service: Offered Services -> Service Name -> Use Service


Librarian
----------
Add a book: Add Book -> Add A Book To The Catalog -> Add Book
Edit a book: Book Catalog -> Title of Book -> Edit Book
Add a device: Add New Device -> Add A Device -> Add Device
Edit a device: Device Catalog -> Device Name -> Edit Device
View list of employees: Library Employees
    

Technician
-----------
Register new member: Members -> Register New Members
Edit room: Rooms -> Library Rooms -> Edit
Add room: Rooms -> Add Room
Edit a service: Service -> Offered Services -> Edit Service
Add a service: Services -> Add New Services
Edit a resource: Resources -> Library Resources -> Edit
Add a resource: Resources -> Add New Resources


Admin
------
Hire new employee: Home → Register New Employee
Edit employee: Home → Employees Index → ‘Edit’ Button
Assign employee’s supervisor: Home → Employees Index → ‘Assign Supervisor’ Button
View reports: Reports Button in Navbar
Add/Edit/View:
-    Books: Refer to Librarian Instructions
-    Devices: Refer to Librarian Instructions
-    Rooms: Refer to Technician Instructions
-    Services: Refer to Technician Instructions
-    Resources: Refer to Technician Instructions
