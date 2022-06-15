import axios from 'axios';
import React, {Component} from 'react'
import Popup from 'reactjs-popup';
class Chat extends Component {
  constructor(props) {
    super(props)
  
    this.state = {
      users: [],
      usernames: [],
      photolinks: [],
      emails: [],
      universities: [],
      descriptions: [],
      yearsofstudy: []
    }
  }

  async componentDidMount() {
    var id = localStorage.getItem("userId");
    var link = "https://findastudybuddymds.azurewebsites.net/api/UserConnections/GetAllFriends?userId="+id;
    
    await axios.get(link)
      .then(response =>
    {
        //console.log(response.data.users);
        this.setState({users: response.data.users});
    })
    .catch(error =>
    {
        console.log(error);
    })
    var names = [];
    var descs = [];
    var years = [];
    var mails = [];
    var links = [];
    var unis = [];
    for (let i=0;i<this.state.users.length; ++i) {
      var data = await axios.get("https://findastudybuddymds.azurewebsites.net/api/User/GetById?id="+this.state.users[i]);
      var name = data.data.data.firstName + " " + data.data.data.lastName;
      var email = data.data.data.email;
      var data1 = await axios.get("https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+this.state.users[i]);
      var desc = data1.data.data.description;
      var year = data1.data.data.yearOfStudy;
      var uni = data1.data.data.university;
      var link3 = "https://findastudybuddymds.azurewebsites.net/api/Photos/Get-Image?userId="+this.state.users[i];
      var photolink = "";
      var data2 = await axios.get(link3)
      photolink = data2.data.data;
      console.log(photolink);
      if (photolink == "") {
        photolink = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
      }
      unis.push(uni);
      links.push(photolink);
      years.push(year);
      mails.push(email);
      descs.push(desc);
      names.push(name);
    }
    this.setState({usernames: names, photolinks: links, yearsofstudy: years, descriptions: descs, universities: unis, emails: mails});
}
  render() {
    var {users, usernames, photolinks, emails, universities, descriptions, yearsofstudy} = this.state;
    //console.log(users);
    var flag = localStorage.getItem("isAuthenticated");
  return (
    <div className='homedescription10'>
      {flag == "true" ?
      <div>
      <h2>Study buddies</h2>
      <br></br>
      {
          users.map(function(d, idx){ 
          //return (<div className="button-logout" value={d}>{usernames[idx]}</div>)
          return (<Popup
            trigger={<button className="button-logout" value={d}>{usernames[idx]}</button>}
            modal
            nested
          >
            {close => (
              <div className="modal">
                <button className="close" onClick={close}>
                  &times;
                </button>
                <div className="header1">
                  <h2 style={{color: "#8b6f47"}}>{usernames[idx]}</h2></div>
                <div className='content'>
                <img className='pozaprofil' src={photolinks[idx]}></img>
                <h3>
                  {/* email: {emails[idx]} */}
                  Description: {descriptions[idx]}
                  <br></br>
                  University: {universities[idx]}
                  <br></br>
                  Year of study: {yearsofstudy[idx]}
                  <br></br>
                  <br></br>
                  <div id="mail" style={{display: "none"}}>
                  <h3 style={{color: "#8b6f47"}}>Email: {emails[idx]}</h3>
                </div>
                </h3>
                </div>
               
                  <button
                    className="button-logout"
                    onClick={() => {
                      console.log(emails[idx]);
                      document.getElementById("mail").style.display = "block";
                    }}
                  >
                    View e-mail
                  </button>
                  <button
                    className="button-logout4"
                    onClick={() => {
                      console.log(localStorage.getItem("userId"), d);
                      axios.delete("https://findastudybuddymds.azurewebsites.net/api/UserConnections/RemoveFriend?userId="+localStorage.getItem("userId")+"&friendId="+d)
                        .then(response => {
                          console.log(response);
                        })
                        .catch(error =>
                          console.log(error));
                        setTimeout(() => {window.location.pathname = "/chat";}, 1000);
                    }}
                  >
                    Delete friend
                  </button>
                <br></br>
              </div>
            )}
          </Popup>)
              })
        }
        </div>
        :
        <div>Please log in to see the content of the app.
          </div>
      }
    </div>
  )
  }
}

export default Chat