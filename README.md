# StackOverflow-Lite
This project is a simplified version of the popular Q&amp;A website, Stack Overflow. It allows users to ask questions, provide answers, and vote on the best answers. This is a great way for learning about web development(ASP.Net MVC Core), databases, and user authentication.

## Project Features: Functional requirements
01. Users can register and login. Email verification has to be added[Worker service & AWS SQS]
02. Users can post questions.
03. Users can use markdown editors to post code in questions.
04. Notification toaster added
05. There will be tags in question
06. Users can view their question List in a table.
07. Users can edit their questions.
08. Users can delete their questions.
09. Users can View their single details.
10. User can search question from the list table
11. User can sort question list based on Title & Tags
12. Authorization added in QuestionController (Need Registration & Login to interact with site functionality)
13. Separate Admin panel & User panel using UserController & Related view pages & models


## Project Features: Non-Functional requirements
01. Project testable, write unit tests for question service
02. Dockerize the application now it'll easily deployable
03. Used clean architecture to maintain flexible design
04. Used both client side and server side validation to maintain security.
05. Used worker service to make the application scalable.
06. Try to use AW SQS Service to send email verification.(Not fixed yet)to make the project sustainable
07. Integrated logger and used exception handling for fault tolerance.
08. Used autofac and automapper to make the code flexible in QuestionCreatingModel file
09. Used entity framework, repository and unit of work pattern to make the project robust.
10. All migrations should be added including seed data to make the project maintainable
11. All menu links have to be correct. User can able to navigate all features(Not completed yet).
12. All completed functional, non function things mentionedin the readme file.

## Project Features: Bonus requirements
01. Clouse toaster messege
02. Get user email & guid in QuestionPostingService
03.
04.
05. 

## Technologies Used
- ASP.NET Core for backend development
- Entity Framework Core for database interaction
- HTML, CSS, and JavaScript for frontend development
- Bootstrap for styling
- SQL Server for database storage

## Project Feture developments:
01. Add Comment on Questions(View & Edit & Delete)
02. Add Answer on Questions(View & Edit & Delete)
03. Add Comment on Answers(View & Edit & Delete)
04. Add Upvote & Downvote on Question & Answers(Single Question)
05. Update User panel & Admin panel
06. Update User banel based on Badge & Reputation

