const form = document.getElementById("loginForm");
const respostaEl = document.getElementById("resposta"); 
form.addEventListener("submit", async (event) => {     
    event.preventDefault();    
  
    try {
        const resposta = await fetch("https://localhost:7006/Usuario/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify({ 
                nome: " ",
                email: document.getElementById("email").value,
                senha: document.getElementById("senha").value})      
        });

        respostaEl.innerText = await resposta.text();   
        
        if (resposta.ok) window.location.href = "tarefa.html";

    } catch (erro) {
        respostaEl.innerText = "Erro ao conectar com o servidor";
        console.error(erro);                            
    }
});

const btnLogout = document.getElementById("Logout");

btnLogout.addEventListener("click", async () => {

    try {

        const resposta = await fetch("https://localhost:7006/Usuario/logout", {
            method: "GET",
            credentials: "include"
        });

        const mensagem = await resposta.text();

        respostaEl.innerText = mensagem;

    } catch (erro) {

        respostaEl.innerText = "Erro ao realizar logout";

        console.error(erro);

    }

});