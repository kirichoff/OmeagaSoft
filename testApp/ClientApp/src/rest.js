const rest ={}

const url = 'https://localhost:44336/'

rest.Modify = function (user,type='update') {
    let typeStr;

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
        method: 'POST',
        body: form,
    })
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}
rest.getUsers = function(){
    return  fetch(url+`api/values/GetAllUsers/`,{
        method: 'GET',
    })
        .then(data =>{console.log(data); return data.json()})
        .catch(ex=> ex)
        ;
}
rest.getJournal = function(){
    return  fetch(url+`api/values/GetJournal/`,{
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
        .then(data => data.json())
        .catch(ex=> ex)
        ;
}

export default rest;
