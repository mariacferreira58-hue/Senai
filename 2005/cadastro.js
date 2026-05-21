const form = document.getElementById("cadastroUsuario");
const respostaEl = document.getElementById("respostaUsuario");
form.addEventListener("submit", async (event) => {
    event.preventDefault();
    
    const sexoSelecionado = document.querySelector('input[name="sexo"]:checked');
    try {
        const resposta = await fetch("https://localhost:7006/Usuario", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify({ 
                nome: document.getElementById("nome").value, 
                email: document.getElementById("email").value, 
                senha: document.getElementById("senha").value})
        });
        respostaEl.innerText = await resposta.text();
        if (resposta.ok) {
            form.reset();
            window.location.href = "login.html";
        }
    } catch (erro) {
        respostaEl.innerText = "Erro ao conectar com o servidor";
        console.error(erro);
    }
});