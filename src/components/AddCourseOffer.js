import React, {Component} from 'react';
import axios from 'axios';
class Addcourseoffer extends Component {
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

    render(){
        
        const {profile, university, yearOfStudy, phoneNumber, description, address, courses, cursuri} = this.state
        var flag = localStorage.getItem("isAuthenticated");
        const obj = profile.data
        var id = localStorage.getItem("userId");
        console.log(id);
        console.log(cursuri)
        return (
          <div>
          {
            flag == "true" ?
            
        <div className='homedescription9'>
          <h1 className='desctext'>Add course for offering help</h1>
          {obj != undefined ?
          <div className='profiledesc'>
            <div className='profileitem'>
                <div className='cursuri'>
                 <div className = 'butoane-need'>
                    {
                        cursuri.map(function(d, idx){
                        //return (<ul key={idx}>{d.helper == true && d.courseName}</ul>)
                        return (<button className="button-logout" onClick={() => {axios.post("https://findastudybuddymds.azurewebsites.net/api/ProfileCourses/Add-Courses?userId="+id, {
                            "courseName": d.courseName,
                            "helper": true,
                            "credit": d.credit
                          }).then(response => {console.log(response)}).then(error => {console.log(error)}); setTimeout(() => {window.location.pathname = "/courses";}, 1000);}} value={d.courseName}>{d.courseName}</button>)
                        })
                  }
                </div>
                </div>
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
export default Addcourseoffer