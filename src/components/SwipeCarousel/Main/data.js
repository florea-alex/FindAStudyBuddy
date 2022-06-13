import axios from 'axios';
const usersId = localStorage.getItem("users").split(",");

async function myfunc () {
  const users = [];
  console.log(usersId);
  for(let i = 0; i<usersId.length; i++) {
    var id = usersId[i];
    var data1 = await axios.get("https://findastudybuddymds.azurewebsites.net/api/User/GetById?id="+id);
    var prenume = data1.data.data.firstName;
    var nume = data1.data.data.lastName;
    var link3 = "https://findastudybuddymds.azurewebsites.net/api/Photos/Get-Image?userId="+id;
    var photolink = "";
    var data2 = await axios.get(link3)
    photolink = data2.data.data;
    if (photolink == "") {
      photolink = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";
    }
    const object = {
      id: id,
      name: prenume + " " + nume,
      source: photolink
    };
    users.push(object);

    
}
console.log(users);
return users;
}

export const usersName = myfunc().then((users) => {return users});
