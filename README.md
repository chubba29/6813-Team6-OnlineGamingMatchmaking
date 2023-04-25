# SWE 6813 - Team6 - Online Gaming Matchmaking Project

## Welcome to our Matchmaking application for SWE 6813 - Spring 2023

Source code begins in the root directory of the Github repo. All sprint deliverables can be found within their respective 'sprint' folders in the root directory of the repo. 

### Below are the links to our front end and back end applications, both hosted on Azure. The Service is where our backend database and .NET Web API are held and the Application is the landing page where users will begin to interface with the app.

## Service:
 * https://gamematching.azurewebsites.net/api/Users

## Application:
 * https://swe6683matchingapp.azurewebsites.net/

The Service link above is an example REST api endpoint and will only display JSON data, but we are including it so anyone interested can see how the backend architecture works by itself. There are plenty of other endpoints to be hit, take a look in the Controllers directory of the source code to find out what all is in there. All endpoints will start with /api after the domain part of the URL.

(We realize the Application domain listed above does not quite match the actual class number, SWE 6813. This was a small error when launching the Azure services and unfotunately is not subject to change without paying money.)

## User Tutorial:

### This app seems to run best with browsers other than Chrome. Edge has worked best in our testing. Chrome will still work, but it is sometimes slow.

Once clicking on the Application link above, users will be met with this landing page: 

![image](https://user-images.githubusercontent.com/57375639/234246378-2882a9f3-0103-45d2-b433-bc8092f1f906.png)

Either create a new account by filling in the desired Username and Password fields and click the CreateUser button OR use our example account with Username = Cliff and Password = Cliff. 

If creating a new account, the app will prompt you to type in a behavior index and a region. We recommend using our example account so that you can get user matches (explained further below).

After clicking the Login button, the user is brought to the Profile page. This is where a user can add 'Game Preferences' to their profile. To do so, click a radio button for a desired game and type in an ELO value for that game. Next, click submit and you should be met with a success alert. This page also allows the user to view their creaeted Game Preferences. Click the 'Load Your Game Preferences' button to query the database for your Game Preferences.

Once happy with the Game Preferences you have tied to your account, click on the User Matching header in the navigation bar at the top of the screen. This screen has a dropdown that is populated with a User's games that they have preferred. Select one and then hit the 'Get Matched Users' button to query the database for other users that have preferred the same game who have the same region and behavior index, as well as a similar ELO value for that game. The matched username and ELO value should show below if such user exists: 

![image](https://user-images.githubusercontent.com/57375639/234249368-701951e8-9633-429c-acd5-13da00c02bc3.png)

Please visit the Service project source code in the github repo for more information on our .NET and REST architecture!


