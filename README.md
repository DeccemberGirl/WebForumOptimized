# WebForumOptimized
<h2>Our project is alredy here, but still does not work properly - we are working hard to correct it as soon as possible.</h2>

<h3>So, DAL is almost ready, only small fixes are needed:</h3>
<ul>
<li>rename parameters to clarify its purpose: for ex. id -> topicId;</li>
<li>move constants to a separate class;</li>
<li>add unit tests where needed.</li>
</ul>
<br>
<h3>BLL functionality is ready, only some refactoring is neeeded:</h3>
<ul>
<li>rename parameters to clarify its purpose: for ex. id -> topicId;</li>
<li>move constants to a separate class;</li>
<li>add unit tests where needed;</li>
<li>add xml-comments;</li>
<li>check formatting - there are no spaces in lots of places;</li>
<li>check usings - not all of them are currently used, sorted and there are repeats of the same using in one file;</li>
<li>split too long strings of code and write only one property on one line, <br>
for ex. var topicDto = new TopicDTO { Date = DateTime.Now.ToString(), UserId = id, UserName = userName, Text = text, Name = name, Messages = new PagedMessagesModel() }; <br>
can be splitted into <br>
var topicDto = new TopicDTO { <br>
Date = DateTime.Now.ToString(), <br>
UserId = id, <br>
UserName = userName, <br>
Text = text, <br>
Name = name, <br>
Messages = new PagedMessagesModel() };</li>
</ul>