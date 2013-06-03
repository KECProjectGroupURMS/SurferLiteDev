###Product Roadmap (SurferLite)###

> Product Roadmap with Releated backlogs

Summary:

**Release 01**

1.	Browser to do simple browsing (Basic working Product, data should pass through service)

	a.	As a user, I want to be able to enter a url, and so that I can browse internet pages.

		i.	Acceptance Criteria:
			1.	There is a place where I can enter url through keyboard.
			2.	On navigate or press enter the internet page should be shown.
	b.	As a user, I don’t have to care about the format of url, so that I can enter my url easily in minimum keyboard strokes.

		i.	Acceptance Criteria:
			1.	I can just enter “google” and https://www.google.com/ is opened.
			2.	Note that in above line, https is automatically known and www and com are added in appropriate locations.
			3.	Also if I enter “google.com” it should be converted to https://www.google.com
			4.	If I enter other invalid urls then the “Error” should be shown. (The detail available only on request.)
	c.	As a user, I would like to see the progress of page loading, so that I know the program is working.

		i.	Acceptance Criteria:
			1.	I can see the progress bar or ring to show the progress.
			2.	Show the progress in percentage also.
	d.	As a user, I would like to see the data size of the page, so that I can know the amount I browsed in this session and till now.

		i.	Acceptance Criteria:
			1.	There should be some statistics recorded to show current page size, current session data size, Total data size.
			
**Release 02**

1.	Browser to show compressed pages
	a.	As a user I want to be able to get compressed pages, so that I use less data to save data usage.

		i.	Acceptance Criteria
			1.	Show actual data and the compressed data size and it should show the compression.
			2.	The compressed size ratio should be less than 80%.
	b.	As a user I want to be able to get compressed images so that I can save more bandwidth.

		i.	Acceptance Criteria
			1.	Images size be compressed. It is usually notable by eye.
			2.	But to know exact compression. The actual Total image size and Total compressed size should be calculated and be shown.
**Release 03**
1.	Show statistics and progress
	a.	As a use, I want to be able to go to statistics and see all the overview of data usage in the page, so that I can get all information at any time just by going to that page.

		i.	Acceptance criteria:
			1.	The page should show Total Data usage, Actual data usage
			2.	This session above information
			3.	This page above info
			4.	Compression ratios
			5.	A graph showing the saved data.
			
**Release 04**

1.	Ability to show or hide images
2.	Select compression quality

**Release 05**

1.	Downloading of files
Release 06
1.	Save browser data after registering users. (BLOB & Storage on Database)

