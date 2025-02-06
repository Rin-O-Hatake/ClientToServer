const express = require('express');
const multer = require('multer');
const bodyParser = require('body-parser');

const PORT = 3000;
const app = express();
const upload = multer();

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.post('/upload', upload.none(), (req, res) => {

    console.log(req.body)
    const username = req.body.username;
    const age = req.body.age;
    
    console.log('Text data:', { username, age });
    
    res.status(200).json({
        status: 'success',
        message: 'File uploaded successfully',
        data: {
            username: username,
            age: age
        }
    });
});

app.listen(PORT, () => {
    console.log('Server is running on http://localhost:' + PORT);
});