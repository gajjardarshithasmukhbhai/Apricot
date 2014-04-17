Apricot
=======

Budget Approval System.
This project is aimed at developing a system by which the employees in the organization submit the bills to their managers. The bills could of various types and also of various amounts. The employee after submitting the bill will automatically provide the manager’s name to which the bill will be submitted. The bill will pass through a workflow process and the owner of the bill can view the status of the bill at any time. An message will be sent to the concerned people to let them know about the status of the bill.


Setup (For Developers)
======================
1. Clone Repository.
2. Update Connection String in app.config. Default Connection String uses localDb that come bundled with visual studio.
3. Setup Database using migrations. Use 'Update-Database' from Package Managr Console. Database will be created based in your Connections.
4. Build and Run.

Test Database can also be used. Use Sample Sb Provided.

Architecture
=============

This is a Three-tier application build with flexibilty and can be easily extended.
This application is composed of three layers :
 
1. Business Logic
2. Data Access Layer
3. Presentation Layer aka Web interface.

This System can be easily extended to have exclusive Mobile App and Other Services. 

For more Info Head on to Documentation.


