import React, { Component } from 'react';
import axios from "axios";

class FileUpload extends Component {

    // API Endpoints
    custom_file_upload_url = `https://findastudybuddy.azurewebsites.net/api/Photos/Add-Image?userId=`+localStorage.getItem("userId");


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
    }

    handleDelete = () => {
        axios.delete(
            "https://findastudybuddy.azurewebsites.net/api/Photos/Delete-Image?userId="+localStorage.getItem("userId"),
            )
            .then(res => {
                console.log(`Success` + res.data);
            })
            .catch(err => {
                console.log(err);
            })
    }


    // render from here
    render() { 
        return (
            <div>

                {/* image input field */}
                <input
                    type="file"
                    onChange={this.handleImagePreview}
                />
                <input type="submit" onClick={this.handleSubmitFile} value="Upload photo"/>
                <input type="submit" onClick={this.handleDelete} value="Delete photo"/>
            </div>
        );
    }
}

export default FileUpload;