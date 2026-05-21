const Myform = document.getElementById("cadastroTarefa");
const respostaEl = document.getElementById("respostaTarefa");
Myform.addEventListener("submit", async (event) => {
    event.preventDefault();
    try {
        const resposta = await fetch("https://localhost:7006/Tarefa", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify({ 
                descricao: document.getElementById("descricao").value, 
                status: document.getElementById("status").value})
        });
        respostaEl.innerText = await resposta.text();
        if (resposta.ok) {
            Myform.reset();
        }
        if (resposta.ok) {

            window.location.href = "consultar.html";

        }
    } catch (erro) {
        respostaEl.innerText = "Erro ao cadastrar tarefa";
        console.error(erro);
    }
});