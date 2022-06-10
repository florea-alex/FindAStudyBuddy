import React, {Component} from 'react';
import axios from 'axios';
import FileUpload from './FileUpload';
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';
class Profile2 extends Component {
    constructor(props) {
      super(props)
    
      this.state = {
        profile: {},
        university: '',
        yearOfStudy: 0,
        phoneNumber: '',
        description: '',
        address: {},
        courses: [],
        cursuri: [],
        nume: '',
        link: '',
        prenume: '',
      }

      //this.handleChange = this.handleChange.bind(this);
    }
    async componentDidMount() {
        var id = localStorage.getItem("userId");
        var link = "https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+id;
        
        await axios.get(link)
      .then(response =>
        {
            console.log(response);
            this.setState({profile: response.data, university: response.data.university, 
            phoneNumber: response.data.phoneNumber, yearOfStudy: response.data.yearOfStudy,
            description: response.data.description, address: response.data.address, courses: response.data.courses})
        })
        .catch(error =>
        {
            console.log(error);
        })

        var data = await axios.get("https://findastudybuddymds.azurewebsites.net/api/BaseCourses/GetAll");
        var cursuri = data.data.data;
        this.setState({cursuri: cursuri});

        var data1 = await axios.get("https://findastudybuddymds.azurewebsites.net/api/User/GetById?id="+id);
        var nume = data1.data.data.lastName;
        var prenume = data1.data.data.firstName;
        this.setState({nume: nume, prenume: prenume});

        var link3 = "https://findastudybuddymds.azurewebsites.net/api/Photos/Get-Image?userId="+id;
        var photolink = "";
        var data2 = await axios.get(link3)
        photolink = data2.data.data;
        localStorage.setItem("link", photolink);
        this.setState({link: photolink});
    }
    
    async updateprofile() {
      //console.log("update");
      const uni = document.getElementById('uni').value;
      const desc = document.getElementById('desc').value;
      const year = parseInt(document.getElementById('year').value);
      const phone = document.getElementById('phone').value;
      const obj = {
        university: uni,
        yearOfStudy: year,
        description: desc,
        phoneNumber: phone
      }

      var id = localStorage.getItem("userId");
      var linkupdate = "https://findastudybuddymds.azurewebsites.net/api/Profile/UpdateUserProfile?userId="+id;

      var dataupdate = await axios.put(linkupdate, obj)

      console.log(dataupdate)

      var link = "https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+id;

      const dataget = await axios.get(link);
      // .then(response =>
      //   {
      //       console.log(response);
      //       // this.setState({profile: response.data, university: response.data.university, 
      //       // phoneNumber: response.data.phoneNumber, yearOfStudy: response.data.yearOfStudy,
      //       // description: response.data.description})
      //   })
      //   .catch(error =>
      //   {
      //       console.log(error);
      //   })

      console.log(dataget);
      const object = dataget.data.data;
      document.getElementById('uni').value = object.university;
      document.getElementById('desc').value = object.description;
      document.getElementById('year').value = object.yearOfStudy;
      document.getElementById('phone').value = object.phoneNumber;
      //console.log(obj);
      setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    async createprofile() {
      //console.log("update");
      const uni = document.getElementById('uni').value;
      const desc = document.getElementById('desc').value;
      const year = parseInt(document.getElementById('year').value);
      const phone = document.getElementById('phone').value;
      const obj = {
        university: uni,
        yearOfStudy: year,
        description: desc,
        phoneNumber: phone
      }

      var id = localStorage.getItem("userId");
      var linkupdate = "https://findastudybuddymds.azurewebsites.net/api/Profile/CreateUserProfile?userId="+id;

      var dataupdate = await axios.post(linkupdate, obj)

      console.log(dataupdate)

      var link = "https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+id;

      const dataget = await axios.get(link);
      // .then(response =>
      //   {
      //       console.log(response);
      //       // this.setState({profile: response.data, university: response.data.university, 
      //       // phoneNumber: response.data.phoneNumber, yearOfStudy: response.data.yearOfStudy,
      //       // description: response.data.description})
      //   })
      //   .catch(error =>
      //   {
      //       console.log(error);
      //   })

      console.log(dataget);
      const object = dataget.data.data;
      document.getElementById('uni').value = object.university;
      document.getElementById('desc').value = object.description;
      document.getElementById('year').value = object.yearOfStudy;
      document.getElementById('phone').value = object.phoneNumber;
      //console.log(obj);
      setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    async addcourse() {

      var data = await axios.get("https://findastudybuddymds.azurewebsites.net/api/BaseCourses/GetAll");
      var obj = data.data.data;
      console.log(obj);
      window.location.pathname='/courses';
    }

    async updateaddress() {
      const county = document.getElementById('county').value;
      const city = document.getElementById('city').value;
      const number = parseInt(document.getElementById('number').value);
      const street = document.getElementById('street').value;
      const obj = {
        county: county,
        city: city,
        street: street,
        number: number
      };
      console.log(obj);
      var id = localStorage.getItem("userId");
      var linkupdate = "https://findastudybuddymds.azurewebsites.net/api/Location/UpdateLocation?userId="+id;

      var dataupdate = await axios.put(linkupdate, obj)
      console.log(dataupdate)
      setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    async createaddress() {
      const county = document.getElementById('county').value;
      const city = document.getElementById('city').value;
      const number = parseInt(document.getElementById('number').value);
      const street = document.getElementById('street').value;
      const obj = {
        county: county,
        city: city,
        street: street,
        number: number
      };
     console.log(obj);
      var id = localStorage.getItem("userId");
      var link = "https://findastudybuddymds.azurewebsites.net/api/Location/AddLocation?userId="+id;
      var datapost = await axios.post(link, obj)
      console.log(datapost)
      setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    async uploadAction() {
      var data = new FormData();
      var imagedata = document.querySelector('input[type="file"]').files[0];
      data.append("data", imagedata);
      for (var pair of data.entries()) {
        console.log(pair[0]+ ', ' + pair[1]); 
    }

      var obj = await axios.post("https://findastudybuddymds.azurewebsites.net/api/Photos/Add-Image?userId="+localStorage.getItem("userId"), data)
      .then(response => {
        console.log(response);
      })
      .then(error => {
        console.log(error);
      })

    }

    async getphoto() {
      var id = localStorage.getItem("userId");
      var link = "https://findastudybuddymds.azurewebsites.net/api/Photos/Get-Image?userId="+id;
      var photolink = "";
      var data = await axios.get(link)
      .then(response => {
        console.log(response);
        photolink = response.data.data;
        this.setState({link: photolink});
      })
      .then(error => {
        console.log(error);
      })

    }

    async handleDelete () {        
      console.log("LSALDCSMLDCKDSMCSD");
      var id = localStorage.getItem("userId");
      localStorage.clear();
      axios.delete("https://findastudybuddymds.azurewebsites.net/api/User/DeleteUser?id="+id)
      .then(response =>
        {
            console.log(response);
        })
        .catch(error =>
        {
            console.log(error);
        });
        setTimeout(() => {window.location.pathname = "/";}, 1000);
    }

    render(){
        const {profile, university, yearOfStudy, phoneNumber, description, address, courses, cursuri, nume, prenume, linkpoza} = this.state
        var flag = localStorage.getItem("isAuthenticated");
        const obj = profile.data
        
        console.log(localStorage.getItem("link"));
        var link = localStorage.getItem("link");
        return (
          <div>
          {
            flag == "true" ?
        <div className='homedescription4'>
          
          <div id = "coloana1">
          <div className='poza'>
            { (link != null && link != "") &&
            <img className='pozaprofil' src={link}></img>
            }
            <h2 style={{color: "#7a3b2e"}}>{prenume} {nume}</h2>
            </div>
          
          <FileUpload />
          </div>
          <div id = "coloana2">
          <div className='profiledesc'><h1 className='desctext'>Profile</h1></div>
          {obj != undefined ?
          <div className='profiledesc'>
            <div className='profileitem1'>
            <div className='profileitem'>
              <h3>University</h3>
              <input id='uni' type="text" defaultValue={obj.university}/>
              {/* <div>{obj.university}</div> */}
            </div>
            <div className='profileitem'>
              <h3>Description</h3>
              <input id='desc' type="text" defaultValue={obj.description} />
            </div>
            </div>
            <div className='profileitem1'>
            <div className='profileitem'>
              <h3>Year of study</h3>
              <input id='year' type="text" defaultValue={obj.yearOfStudy} />
            </div>
            <div className='profileitem'>
              <h3>Phone number</h3>
              <input id='phone' type="text" defaultValue={obj.phoneNumber} />
            </div>
            </div>
            <div className='profileitem'>
              <h3>Address</h3>
              {obj.address != null ?
                <div className='cursuri'>
                  <div>
                  <label>County&nbsp;<input id='county' type="text" defaultValue={obj.address.county}/></label>
                 <label>City&nbsp;<input id='city' type="text" defaultValue={obj.address.city}/></label>
                 </div><div>
                 <label>Street&nbsp;<input id='street' type="text" defaultValue={obj.address.street}/></label>
                 <label>Number&nbsp;<input id='number' type="text" defaultValue={obj.address.number}/></label>
                  </div>
                </div>
              :
               <div className='cursuri'>
                 <div>
                 <label>County&nbsp;<input id='county' type="text" placeholder='-'/></label>
                 <label>&nbsp;&nbsp;City&nbsp;<input id='city' type="text" placeholder='-'/></label>
                 </div><div>
                 <label>Street&nbsp;<input id='street' type="text" placeholder='-'/></label>
                 <label>&nbsp;&nbsp;Number&nbsp;<input id='number' type="text" placeholder='-'/></label>
               </div>
               </div>
              }
            </div>
            {/* <div className='profileitem'>
              <h3 className='cursuri'><div>Courses (name, no. of credits)</div></h3>
              {obj.courses.length != 0 ?
                <div className='cursuri'>
                  {
                    obj.courses.map(function(d, idx){
                      return (<ul key={idx}>{d.courseName}, {d.credit}</ul>)
                    })
                  }
                </div>
              :
                <div>-</div>
              }
            </div> */}
            
            <br></br>
            <div className='buttonsprofile'>
            {obj.address != null ?
            <div>
              <button className="button-update" onClick={this.updateaddress}>Update address</button>
            </div>
            :
            <div>
              <button className="button-update" onClick={this.createaddress}>Add address</button>
            </div>
            }
            <div>
              <button className="button-update1" onClick={this.updateprofile}>Update profile</button>
            </div>
            <div>
              <button className="button-update" onClick={this.addcourse}>Go to courses</button>
            </div>
            {/* <div>
              <button className="button-update" onClick={this.getphoto}>Get photo</button>
            </div> */}
            </div>
            <Popup
              trigger={<button className="button-logout4">Delete account</button>}
              modal
              nested
            >
              {close => (
                <div className="modal">
                  <button className="close" onClick={close}>
                    &times;
                  </button>
                  <div className="header1">
                    <h2>Delete account</h2></div>
                  <div className='content'>
                  <h3>
                    Are you sure you want to delete your account?
                  </h3>
                  </div>
                  <div className="actions">
                  <button
                      className="button-logout4"
                      onClick={this.handleDelete}
                    >
                    Yes
                    </button>
                    <button
                      className="button-logout5"
                      onClick={() => {
                        console.log('modal closed ');
                        close();
                      }}
                    >
                      No
                    </button>
                  </div>
                </div>
              )}
            </Popup>
            {/* <div>
            <button className="button-logout4" onClick={this.handleDelete}>Delete account</button>
            </div> */}
            
          </div>
        :
        <div className='profiledesc'>
          <div className='profileitem1'>
            <div className='profileitem'>
              <h3>University</h3>
              <input id='uni' type="text" placeholder='-'/>
              {/* <div>{obj.university}</div> */}
            </div>
            <div className='profileitem'>
              <h3>Description</h3>
              <input id='desc' type="text" placeholder='-'/>
            </div>
            </div>
            <div className='profileitem1'>
            <div className='profileitem'>
              <h3>Year of study</h3>
              <input id='year' type="text" placeholder='-'/>
            </div>
            <div className='profileitem'>
              <h3>Phone number</h3>
              <input id='phone' type="text" placeholder='-'/>
            </div>
            </div>
            <div className='profileitem'>
              <h3>Address</h3>
              <div className='cursuri'>
                 <div>
                 <label>County&nbsp;<input id='county' type="text" placeholder='-'/></label>
                 <label>&nbsp;&nbsp;City&nbsp;<input id='city' type="text" placeholder='-'/></label>
                 </div><div>
                 <label>Street&nbsp;<input id='street' type="text" placeholder='-'/></label>
                 <label>&nbsp;&nbsp;Number&nbsp;<input id='number' type="text" placeholder='-'/></label>
               </div>
               </div>
            </div>
            {/* <div className='profileitem'>
              <h3 className='cursuri'><div>Courses (name, no. of credits)</div></h3>
                <div className='cursuri'>
                  -
                </div>
            </div> */}
            
            <br></br>
            <div className='buttonsprofile'>
            <div>
              <button className="button-update" onClick={this.createaddress}>Add address</button>
            </div>
            <div>
              <button className="button-update1" onClick={this.createprofile}>Create profile</button>
            </div>
            <div>
              <button className="button-update" onClick={this.addcourse}>Go to courses</button>
            </div>
            </div>
            <Popup
              trigger={<button className="button-logout4">Delete account</button>}
              modal
              nested
            >
              {close => (
                <div className="modal">
                  <button className="close" onClick={close}>
                    &times;
                  </button>
                  <div className="header1">
                    <h2>Delete account</h2></div>
                  <div className='content'>
                  <h3>
                    Are you sure you want to delete your account?
                  </h3>
                  </div>
                  <div className="actions">
                  <button
                      className="button-logout4"
                      onClick={this.handleDelete}
                    >
                    Yes
                    </button>
                    {/* <Popup
                      trigger={<button className="button-logout4" onClick={this.handleDelete}>Yes</button>}
                      position="top center"
                    >
                    </Popup> */}
                    <button
                      className="button-logout5"
                      onClick={() => {
                        console.log('modal closed ');
                        close();
                      }}
                    >
                      No
                    </button>
                  </div>
                </div>
              )}
            </Popup>
          </div>
        }
        </div>
        </div>
      :
      <div className='homedescription'>
          <h1 className='desctext'>Profile</h1>
          <br></br>
          <br></br>
          <div>Please log in to see the content of the app.</div>
        </div>
      }
      </div>
        );
    }
}
export default Profile2