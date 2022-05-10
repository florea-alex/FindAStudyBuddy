import React, {Component} from 'react';
import axios from 'axios';
class Profile2 extends Component {
    constructor(props) {
      super(props)
    
      this.state = {
        profile: {}
      }
    }
    componentDidMount() {
        var link = "https://findastudybuddy.azurewebsites.net/api/Profile/GetById?userId="+1;
        
        axios.get(link)
      .then(response =>
        {
            console.log(response);
            this.setState({profile: response.data})
        })
        .catch(error =>
        {
            console.log(error);
        })
    }
    render(){
        const {profile} = this.state
        var flag = localStorage.getItem("isAuthenticated");
        const obj = profile.data
        return (
          <div>
          {
            flag == "true" ?
        <div className='homedescription'>
          <h1 className='desctext'>Profile</h1>
          {obj != undefined ?
          <div>
            <div>university = {obj.university}</div>
            <div>year of study = {obj.yearOfStudy}</div>
            <div>description = {obj.description}</div>
            <div>phone number = {obj.phoneNumber}</div>
            {obj.courses.length != 0 ?
            <div>cursuri</div>
            :
            <div>nu are cursuri</div>
            }
            {obj.address != null ?
            <div>adresa</div>
            :
            <div>nu are adresa</div>
            }
          </div>
        :
        <div>Creati profilul!</div>  
        }
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