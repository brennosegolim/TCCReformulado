function ValidaCPF() {

	var RegraValida = document.getElementById("RegraValida").value;
	var cpfValido = /^(([0-9]{3}.[0-9]{3}.[0-9]{3}-[0-9]{2})|([0-9]{11}))$/;

	if (cpfValido.test(RegraValida) == true) {

		console.log("CPF Válido");

	} else {

		console.log("CPF Inválido");

	}

}

function ValidaRG() {

	var RegraValida = document.getElementById("RegraValida").value;
	var rgValido = /^(([0-9]{2}.[0-9]{3}.[0-9]{3}-[0-9]{1})|([0-9]{11}))$/;

	if (rgValido.test(RegraValida) == true) {

		console.log("RG Válido");

	} else {

		console.log("RG Inválido");

	}

}

function ValidaCEP() {

	var RegraValida = document.getElementById("RegraValida").value;
	var cepValido = /^(([0-9]{5}-[0-9]{3}))$/;

	if (cpfValido.test(RegraValida) == true) {

		console.log("CEP Válido");

	} else {

		console.log("CEP Inválido");

	}

}

function fMasc(objeto, mascara) {

	obj = objeto
	masc = mascara

	setTimeout("fMascEx()", 1)

}

function fMascEx() {

	obj.value = masc(obj.value)

}

function mCPF(cpf) {

	cpf = cpf.replace(/\D/g, "")
	cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
	cpf = cpf.replace(/(\d{3})(\d)/, "$1.$2")
	cpf = cpf.replace(/(\d{3})(\d{1,2})$/, "$1-$2")
	return cpf

}

function mRg(rg) {

	rg = rg.replace(/\D/g, '');
	rg = rg.replace(/^(\d{2})(\d)/g, "$1.$2");
	rg = rg.replace(/(\d{3})(\d)/g, "$1.$2");
	rg = rg.replace(/(\d{3})(\d)/g, "$1-$2");
	return rg;

}

function mCep(cep) {

	cep = cep.replace(/\D/g, "")
	cep = cep.replace(/^(\d{5})(\d)/, "$1-$2")
	return cep

}

function mData(data) {
	data = data.replace(/\D/g, "")
	data = data.replace(/(\d{2})(\d)/, "$1/$2")
	data = data.replace(/(\d{2})(\d)/, "$1/$2")
	return data
}

function mtel(v) {
	v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
	v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
	v = v.replace(/(\d)(\d{4})$/, "$1-$2"); //Coloca hífen entre o quarto e o quinto dígitos
	return v;
}