----------
###*As a user, I can browse internet in low bandwidth, so that I can save money on volume based internet package.*###

**Motivation**

If we can lower the data usage on internet browsing using some compression of the requested page on our server after downloading page then client can get compressed page and save money. Users will use this only if this is possible.

**Acceptance Criteria**

- The software is Windows 8 store app.
- The client software can be installed without doing much work.
- Easy to start browsing internet after starting the app.

**Technical**

The work of compression is done in Azure Cloud server. So the server program is to developed.

----------

###*As a user, I want to be able to configure desired compression level so that I can specify how compressed the downloaded data should be.*###

**MOTIVATION**

High level of compression can significantly reduce the bandwidth usage for the user thus allowing him to save money. However, it also slows down the internet because it requires more time to compress more. The goal of this feature is to allow users to make right balance between the level of compression and speed he needs.

**ACCEPTANCE CRITERIA**

- When the user clicks on 'setting' menu, he should see an option called 'Compression ratio'.
- When the user clicks on 'compression ratio', a modal box should show up. The dialog box should show three options: High, Medium, Low.
- The user should be able to select one of these options using a slider.
- When user selects an option and clicks OK, the compression level in the server should be set accordingly.