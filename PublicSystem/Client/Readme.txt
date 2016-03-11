This is is the client side piece of the public facing website, where anyone can access
the information in the Govmeeting database.

STEP 1 - simple demo of the concept.
This consists of 4 files demo.html, demo.js,demo.css and BBH-2014-09-08.json.
* demo.js defines a single controller "govmeeting". This contains all of the
  data in the model in the variable $scope.meeting. It also contains two methods for filtering 
  the data by topic and speaker.
* demo.html displays all the the data within the model and contains user controls for 
  filtering the data.
* demo.css styles the page with two columns, control left and data right.
* testdata\\BBH-2014-09-08.json contains JSON test data representing a single meeting.
  It is read directly from the file via the $http service.
The demo code does not bother to observe best practices. The angular controller is defined
in the global namespace. There are no modules defined. All the code is in the controller.

Step 2 - Start adding some best practices.
* Create two modules
	 "mainApp" - encompassing module for the entire app
	 "singleMeeting" - feature to show info for a single meeting
* Change to a typescript project. For now we just change the .JS endings to .TS
  and use NuGet to add AngularJS and it and JQuery's definition files.

  Step 3
* Create a service for accessing the backend data.

// TODO test