import authHelper from "./utils/authHelper";
const rest ={}
const url = process.env.URL || 'https://localhost:44336/';



rest.Modify = function (user,type='update') {
    let typeStr;
    let token = authHelper.getToken();
    console.log('from modify',token)
    switch (type){
        case 'update': typeStr = 'UpdateUser'; break;
        case 'add': typeStr = 'AddUser'; break;
        case 'delete': typeStr = 'RemoveUser'; break;
        default: typeStr = 'UpdateUser';
    }

    let iso =  new  Date();
    let date = iso.toISOString();
    let form = new FormData()

    form.append('user',  JSON.stringify(user))
    return  fetch(url+`api/values/${typeStr}`,{
        headers:{
            'Authorization': 'Bearer ' + token
        },
        method: 'POST',
        body: form,
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}
rest.getUsers = function(){
    let token = authHelper.getToken();
    return  fetch(url+`api/values/GetAllUsers/`,{
        headers:{
            'Authorization': 'Bearer ' + token
        },
        method: 'GET',
    })
        .then(data =>{console.log(data); return data.json()})
        .catch(ex=> ex)
        ;
}
rest.getJournal = function(){

    let token = authHelper.getToken();
    return  fetch(url+`api/values/GetJournal/`,{
        headers:{
            'Authorization': 'Bearer ' + token
        },
        method: 'GET',
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}

rest.getToken = function(username,password){
    return  fetch(url+`api/Token/Token/?name=${username}&password=${password}`,{
        method: 'GET',
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}
rest.GetUser = function () {
    let token = authHelper.getToken();
    let username=authHelper.getLogin();
    return  fetch(url+`api/values/GetUser?name=${username}`,{
        headers:{
            'Authorization': 'Bearer ' + token
        },
        method: 'GET',
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}
rest.find = function (username,password) {
    return  fetch(url+`api/values/Login/?name=${username}&password=${password}`,{
        method: 'GET',
    })
        .then(data =>  data.json())
        .catch(ex=> ex)
        ;
}

export default rest;
