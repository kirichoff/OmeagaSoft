export const sort ={}

sort.byName=function (array,name) {
    array.sort((a,b)=> {
        if (b.UserName === name)return 0
        if (b.UserName !== name  )return -1
        if (b.UserName === name) return 1
        }
    )
}
sort.byAction = function (array,action) {
    array.sort((a,b)=> {
            if (b.Action === action)return 0
            if (b.Action !== action  )return -1
            if (b.Action === action) return 1
        }
    )
}
sort.byActionAndName = function (array,action,name) {
    array.sort((a,b)=> {
            if ((b.Action === action) && (b.UserName === name))return 0
            if ((b.Action !== action)  && (b.UserName !==  name))return -1
            if ((b.Action === action) && (b.UserName === name))  return 1
        }
    )
}
sort.ByDate = function (array) {
    array.sort((a,b)=>{
        if(a.Date<b.Date) return-1
        if(a.Date>b.Date) return 1
        if(a.Date===b.Date) return 0
    })
}
