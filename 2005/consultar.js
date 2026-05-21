const form = document.getElementById("formConsultaTarefa");
const respostaEl = document.getElementById("respostaTarefa");
form.addEventListener("submit", async (event) => {
    event.preventDefault();
    try {
        const resposta = await fetch("https://localhost:7006/Tarefa/tarefasUsuario", {
            method: "GET",
            credentials: "include"
        });

        if (!resposta.ok) {

            respostaEl.innerText = await resposta.text();
            return;

        }
        if (resposta.ok) {
           // form.reset();
           // window.location.href = "consultar.html";
        }

        const tarefas = await resposta.json();

        console.log(tarefas); 

        respostaEl.innerHTML = "";

        tarefas.forEach(tarefa => {

            respostaEl.innerHTML += `
                <div>
        
                   tarefa: <input type='text' id='descricao${tarefa.tarefas}' value='
                        ${tarefa.descricao}
                    '>
        
                    status: <input type='text' id='status${tarefa.tarefas}' value='
                        ${tarefa.status}
                    '>
        
                    <p>
                        <strong>ID da Tarefa:</strong>
                        ${tarefa.tarefas}
                    </p>
        
                    <button onclick="deletaTarefa(${tarefa.tarefas})">
                        Deletar
                    </button>

                    <button onclick="atualizaTarefa(${tarefa.tarefas})">
                        Atualizar
                    </button>
        
                    <hr>
        
                </div>
            `;
        });

    } catch (erro) {

        respostaEl.innerText = "Erro ao buscar tarefas";
        console.error(erro);

    }

});

async function deletaTarefa(IdTarefa) {

    try {

        const resposta = await fetch(`https://localhost:7006/Tarefa/${IdTarefa}`, {
            method: "DELETE",
            credentials: "include"
        });

        const mensagem = await resposta.text();

        alert(mensagem);

        location.reload();

    } catch (erro) {

        console.error(erro);

    }

}

function atualizaTarefa(IdTarefa){

    fetch('https://localhost:7006/Tarefa/' + IdTarefa, {
    
    method: 'PUT',
    
    credentials: 'include',
    
    headers: {
    'Content-Type': 'application/json',
    },
    
    body: JSON.stringify({
    
    descricao: document.getElementById("descricao" + IdTarefa).value,
    
    status: document.getElementById("status" + IdTarefa).value,
    
    }),
    
    })
    
    .then(response => {
    
    if (response.status == 401){
    
    alert("Faça login antes de editar!");
    
    window.location.href = "consultar.html";
    
    } else {
    
    alert("Tarefa editada!");
    
    }
    
    })
    
    }