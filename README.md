# PrintingHouse
<p>ASP.Net CORE MVC Project for SoftUni ASP.NET Advanced course </p>
<p>A simple inhouse web application for monitoring and controlling the production process in a printing house for advertising and commercial materials. </p>
<p>It's designed to be used only from employees.</p>

# Technologies
 	.NET Core 6.0
 	ASP.NET Core
 	Entity Framework Core    
	MS SQL Server
	MinIO object storage
	HTML, CSS, Bootstrap
	JS, JQuery, AJAX
 	NUnit, Moq


There are 4 types of roles that can be given from Administrator or someone else with role "Admin": Admin, Printer, Merchant and Employee.

  - The administrator can create a working positions and add already registered user to a position and give him a role. Also can reasign or fire employee in the administration area.  
  - The Printer can start and stop(complete) an order
  - The Merchant can add new clients and create articles and orders
  - The employees with admin role can create and cancel orders. Also can buy materials and colors.
  
