# Find A Study Buddy
We designed and developed a Web Application for students which allows users to create and customize their account. The courses linked to a student’s profile are split into two categories: courses for which he/she can offer help and courses for which he/she needs help from others. An user can choose the users they want to connect with, by swiping right when their profile appears on the Dashboard page. If two students match, they now have a connection and can talk via chat and they can end their connection if they choose to.

# Members 
---
@dianacordun
@AndrewSSB
@florea-alex
@vladfxstoader

# User Stories
---
1. As a user, I want to be able to create an account.

    - **Acceptance criteria:**
      - should implement a functional "create account" button which leads the user on a new page
      - should have text box for name/surname, email, password, university (optional at this stage), user's current year of study (optional at this stage), address (optional throughout the experience), phone number (optional throughout the experience), a short description (maximum 500 characters, mandatory at this stage), the courses the user needs help (optional at this stage)
      - should check if the user's email is valid (has @generic.com, does not contain characters like: ",;\:/<$>%&*#~".
      - should check if the password is valid: at least 10 characters, at least one uppercase letter, at least one character
      - should have the possibility to remember your account by clicking "remember my password" button from the landing page

2. As a newly registered user, I want to complete my profile.

      - **Acceptance criteria:**
        - should let the newly registered users upload different details about themselves: university (mandatory at this stage), user's current year of study (mandatory at this stage), a short description (maximum 500 characters, mandatory at this stage), address (still optional), phone number (still optional),  the courses they need help (optional at this stage)

3. As an user with an existing account, I want to sign into the app so that I can connect with the other users.

      - **Acceptance criteria:**
        - should have on the landing page a functional button for "login" next to the "create account"/"sign up" button
        - login is done with email and password
        - logging in should be allowed only to already existing users (users that have already created their account)
        - at this stage there is also a "forgot your password?" button which after clicking it leads you to a new page where you can write the email of your existing account, where you will receive an email for the password reset
        - once you are are signed into the application, you can start chatting with the people you feel connected to

4. As a logged user, I want to chat with people with similar interests.

      - **Acceptance criteria:**
        - should have the "matching algorithm" implemented so an user can find people with similar interests
        - an user can choose if they want to chat or not with the current suggested person in the tab "find new buddies"
        - should exist a "messages" button where you can find an user's conversations with all their connections
        - after going to an user's conversation there should be a list with your existing connections and chats, the chat you clicked most recently
        - after selecting a specific chat, there is a text box to write a message, buttons for attachments (.png, .pdf, .doc, .docx, .txt) and the user has the possibility to go on the other user's profile
        - by swiping right/left an user will have the right-swiped students in the chat page which can be accessed by clicking on the bottom left icon in the dashboard page

5. As an user, I want to update my profile anytime.

      - **Acceptance criteria:**
        - should have the possibility to change the password by providing the email address and the old password
        - should provide the option to update all fields (besides name/surname and email), but all the mandatory fields must remain completed, while still having the possibility to update them
        - should have the permission to add new courses where the user needs help
        - should have the possibility to update/delete my help section

6. As an user, I want to be able to delete my connections with people I don't want to talk to anymore.

      - **Acceptance criteria:**
        - should have a "unmatch/disconnect" button; after an user presses it, that connection is deleted
        - users can close/exit the chat with another user anytime, without losing any messages (real-time chat)
        - after an user deletes their connection with another user, their profile should be removed from that user's list of connections
        - when an user unmatches someone, that person should not appear anymore as a suggestion for connections.
        - an error 404 will be displayed when a person you unmatched tries to access your profile

7. As a logged user, I want to be able to sign out/log out anytime.

      - **Acceptance criteria:**
        - on the main page/the profile page of every user, there should be a "sign out/log out" button
        - when pressed, it redirects them to the landing page

8. As an existing user, I want to be capable of deleting my account anytime.

      - **Acceptance criteria:**
        - the user will be able to delete their account by pressing a button
        - the button will be located in the profile updating area
        - all their data will be lost 
        - the deleted profile won't be available for the other users
        - an error 404 will be displayed when another user tries to see the deleted profile

9. 9. As an user with chats, I want to be able to access my dashboard whenever I would like to.

      - **Acceptance criteria**
        - an user would be able to access their dashboard whenever they are logged and go on the dashboard page
        - no other constraints should be present

10. As an existing user, I want to be able to manage my own connections.

      - **Acceptance criteria**
        - when an user is on the dashboard page, they can easily choose their connection by only swiping right (if they would like to chat with that person) or by swiping left (if they are not sure about making a connection with that person)

11. As a user, I would like to receive and offer help anytime.

      - **Acceptance criteria**
        - should implement a section on the profile with “give help/receive help”, thus letting other users know that the person is in need or wants to help
        - depending on the user’s choice, this will improve the matching between me and people with similar interests (ex. if an user has the option "offer help" on their profile, they will mostly be matched with people that want to receive help)
        - the user should be able to update that section (choose between receiving or giving help or both)

12. As a newly registered user, I would like to receive a confirmation email.

      - **Acceptance criteria**
        - should create an email address which sends automatically a generic confirmation email for logging in (findastudybuddy2022@gmail.com)
        - should use a Sendgrid API

13. As a newly registered user, I would like to receive a registration email.

      - **Acceptance criteria**
        - should create an email address which sends automatically a generic confirmation email for registration (findastudybuddy2022@gmail.com)
        - should use a Sendgrid API
