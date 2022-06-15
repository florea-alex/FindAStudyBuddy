import React, {Component} from 'react';
import axios from 'axios';
class Courses extends Component {
    constructor(props) {
      super(props)
    
      this.state = {
        profile: [],
        university: '',
        yearOfStudy: 0,
        phoneNumber: '',
        description: '',
        address: {},
        courses: [],
        cursuri: []
      }


      //this.handleChange = this.handleChange.bind(this);
    }

    handleInput(e) {
        console.log(e.target.value);
    }

    async componentDidMount() {
        var id = localStorage.getItem("userId");
        var link = "https://findastudybuddymds.azurewebsites.net/api/Profile/GetById?userId="+id;
        
        await axios.get(link)
      .then(response =>
        {
            //console.log(response);
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
    }

    async addcourseneed() {

    //   var data = await axios.get("https://findastudybuddy.azurewebsites.net/api/BaseCourses/GetAll");
    //   var obj = data.data.data;
    //   console.log(obj);

    //   let p = document.createElement("p");
    //   p.textContent = "NEED";
    //   document.getElementById('butoane-need').style.display = 'block';

    window.location.pathname='/addcourseneed';
    }

    async addcourseoffer() {

        // var data = await axios.get("https://findastudybuddy.azurewebsites.net/api/BaseCourses/GetAll");
        // var obj = data.data.data;
        // console.log(obj);

        // document.getElementById('butoane-offer').style.display = 'block';
        window.location.pathname='/addcourseoffer';
      }


    render(){
        
        const {profile, university, yearOfStudy, phoneNumber, description, address, courses, cursuri} = this.state
        var flag = localStorage.getItem("isAuthenticated");
        const obj = profile.data
        var id = localStorage.getItem("userId");
        console.log(cursuri)
        return (
          <div>
          {
            flag == "true" ?
        <div className='homedescription'>
          <h1 className='desctext'>Courses</h1>
          {obj != undefined ?
          <div className='profiledesc'>
            <div className='profileitem'>
              {obj.courses.length != 0 ?
                <div className='cursuri'>
                <div className='cursuri-need' id='need'>
                    <h3>Need help</h3>
                  {
                    obj.courses.map(function(d, idx){
                      return (<ul key={idx}>{d.helper == false && d.courseName}</ul>)
                    })
                  }
                <br></br>

                  <div>
                    <button className="button-update1" onClick={this.addcourseneed}>Add course</button>
                 </div>
                 <div id = 'butoane-need' style={{display: 'none'}}>
                    {
                        cursuri.map(function(d, idx){
                        //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                        return (<button onClick={() => {axios.post("https://findastudybuddymds.azurewebsites.net/api/ProfileCourses/Add-Courses?userId="+id, {
                            "courseName": d.courseName,
                            "helper": false,
                            "credit": d.credit
                          }); setTimeout(() => {window.location.reload(false)}, 1000);}} value={d.courseName}>{d.courseName}</button>)
                        })
                }
                </div>
                 <br></br>
                 </div>
                 <div className='cursuri-offer' id='offer'>
                    <br></br>
                    <h3>Offer help</h3>

                  {
                    obj.courses.map(function(d, idx){
                      return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                    })
                  }
                <br></br>
                <div>
                    <button className="button-update1" onClick={this.addcourseoffer}>Add course</button>
                 </div>
                 <div id = 'butoane-offer' style={{display: 'none'}}>
                    {
                        cursuri.map(function(d, idx){
                        //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                        return (<button onClick={() => {axios.post("https://findastudybuddymds.azurewebsites.net/api/ProfileCourses/Add-Courses?userId="+id, {
                            "courseName": d.courseName,
                            "helper": true,
                            "credit": d.credit
                          }); setTimeout(() => {window.location.reload(false)}, 1000);}} value={d.courseName}>{d.courseName}</button>)
                        })
                }
                </div>
                 </div>
                </div>
                
              :
              <div className='cursuri'>
              <div className='cursuri-need' id='need'>
                  <h3>Need help</h3>
                -
              <br></br>

                <div>
                  <button className="button-update1" onClick={this.addcourseneed}>Add course</button>
               </div>
               <div id = 'butoane-need' style={{display: 'none'}}>
                  {
                      cursuri.map(function(d, idx){
                      //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                      return (<button onClick={() => {axios.post("https://findastudybuddymds.azurewebsites.net/api/ProfileCourses/Add-Courses?userId="+id, {
                          "courseName": d.courseName,
                          "helper": false,
                          "credit": d.credit
                        }); setTimeout(() => {window.location.reload(false)}, 1000);}} value={d.courseName}>{d.courseName}</button>)
                      })
              }
              </div>
               <br></br>
               </div>
               <div className='cursuri-offer' id='offer'>
                  <br></br>
                  <h3>Offer help</h3>
                -
              <br></br>
              <div>
                  <button className="button-update1" onClick={this.addcourseoffer}>Add course</button>
               </div>
               <div id = 'butoane-offer' style={{display: 'none'}}>
                  {
                      cursuri.map(function(d, idx){
                      //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                      return (<button onClick={() => {axios.post("https://findastudybuddymds.azurewebsites.net/api/ProfileCourses/Add-Courses?userId="+id, {
                          "courseName": d.courseName,
                          "helper": true,
                          "credit": d.credit
                        }); setTimeout(() => {window.location.reload(false)}, 1000);}} value={d.courseName}>{d.courseName}</button>)
                      })
              }
              </div>
               </div>
              </div>
              }
            </div>
            
            <br></br>
            {/* {cursuri != [] ?
            <div>
            {
                cursuri.map(function(d, idx){
                //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                return (<button>{d.courseName}</button>)
                })
            }
            </div>
            :
            <div>
            </div>
            } */}
          </div>
        :
        <div className='profiledesc'>
            <div className='profileitem'>
                <div className='cursuri'>
                <h3>Need help</h3>
                -
                    <br></br>
                    <h3>Offer help</h3>
                -
                </div>
            </div>
            
            <br></br>
            <div className='buttonsprofile'>
            <div>
              <button className="button-update1" onClick={this.addcourse}>Add course</button>
            </div>
            </div>
          </div>
        }
        </div>
      :
      <div className='homedescription'>
          <h1 className='desctext'>Courses</h1>
          <br></br>
          <br></br>
          <div>Please log in to see the content of the app.</div>
        </div>
      }
      </div>
        );
    }
}
export default Courses