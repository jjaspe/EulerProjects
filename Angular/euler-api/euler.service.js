import problems from './problems/index'

export default {
    solve: (id) => problems["problem"+id](),

    solveWithParams: (id, param) => {
        var problemName = "problem"+id;
        return problems[problemName](param)
    }
}

    

    


