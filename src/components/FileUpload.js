import React, { Component } from 'react';
import axios from "axios";

class FileUpload extends Component {

    // API Endpoints
    custom_file_upload_url = `https://findastudybuddymds.azurewebsites.net/api/Photos/Add-Image?userId=`+localStorage.getItem("userId");


    constructor(props) {
        super(props);
        this.state = {
            image_file: null,
            image_preview: '',
        }
    }

    // Image Preview Handler
    handleImagePreview = (e) => {
        let image_as_base64 = URL.createObjectURL(e.target.files[0])
        let image_as_files = e.target.files[0];

        this.setState({
            image_preview: image_as_base64,
            image_file: image_as_files,
        })
    }

    // Image/File Submit Handler
    handleSubmitFile = () => {

        if (this.state.image_file !== null){

            let formData = new FormData();
            formData.append('Files', this.state.image_file);
            // the image field name should be similar to your api endpoint field name
            // in my case here the field name is customFile

            axios.post(
                this.custom_file_upload_url,
                formData,
                {
                    headers: {
                        "Content-type": "multipart/form-data",
                    },                    
                }
            )
            .then(res => {
                console.log(`Success` + res.data);
            })
            .catch(err => {
                console.log(err);
            })
        }
        setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    handleSubmitFileUpdate = () => {

        if (this.state.image_file !== null){

            let formData = new FormData();
            formData.append('Files', this.state.image_file);
            // the image field name should be similar to your api endpoint field name
            // in my case here the field name is customFile

            axios.put(
                "https://findastudybuddymds.azurewebsites.net/api/Photos/Update-Image?userId="+localStorage.getItem("userId"),
                formData,
                {
                    headers: {
                        "Content-type": "multipart/form-data",
                    },                    
                }
            )
            .then(res => {
                console.log(`Success` + res.data);
            })
            .catch(err => {
                console.log(err);
            })
        }
        //setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }

    handleDelete = () => {
        axios.delete(
            "https://findastudybuddymds.azurewebsites.net/api/Photos/Delete-Image?userId="+localStorage.getItem("userId"),
            )
            .then(res => {
                console.log(`Success` + res.data);
            })
            .catch(err => {
                console.log(err);
            })
            localStorage.removeItem("link");
        setTimeout(() => {window.location.pathname = "/profile";}, 1000);
    }


    // render from here
    render() { 
        var link = localStorage.getItem("link");
        console.log("link is: "+link);
        return (
            <div className='profiledesc3'>

                {/* image input field */}
                
                {(link != null && link != "") ?
                //<input type="submit" onClick={this.handleSubmitFileUpdate} value="Update photo"/>
                <input type="submit" onClick={this.handleDelete} value="Delete photo"/>
                :
                <div className='profiledesc3'>
                    <input
                        type="file"
                        onChange={this.handleImagePreview}
                    />
                    <input type="submit" onClick={this.handleSubmitFile} value="Upload photo"/>
                </div>
                }
                
            </div>
        );
    }
}

export default FileUpload;