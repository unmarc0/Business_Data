html {
    font - size: 14px;
    position: relative;
    min - height: 100 %;
}

@media(min - width: 768px) {
    html {
        font - size: 16px;
    }
}

body {
    margin - bottom: 60px;
    font - family: 'Arial', sans - serif;
    background - color: #f4f6f7; /* Color más neutro y limpio */
    color: #333;
    padding - top: 20px;
}

/* Encabezado */
header {
    background - color: #2c3e50; /* Color oscuro y elegante */
    color: white;
    text - align: center;
    padding: 20px 0;
    font - family: 'Georgia', serif;
    box - shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

header h1 {
    font - size: 2.5em;
    margin: 0;
}

header h1 span {
    color: #1abc9c; /* Color turquesa que resalta */
}

footer {
    background - color: #2c3e50;
    color: white;
    text - align: center;
    padding: 15px 0;
    position: absolute;
    width: 100 %;
    bottom: 0;
    font - family: 'Georgia', serif;
}

footer p {
    font - size: 1.2em;
}

/* Títulos */
h1, h2, h3, h4, h5, h6 {
    color: #1abc9c; /* Color verde turquesa */
    font - family: 'Georgia', serif;
}

/* Estilos de las gráficas */
.chart - grid {
    display: grid;
    grid - template - columns: repeat(2, 1fr);
    gap: 20px;
    margin - top: 40px;
}

.chart - box {
    border - radius: 10px;
    box - shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    background - color: #fff;
    padding: 15px;
    text - align: center;
    transition: transform 0.3s ease, box - shadow 0.3s ease;
}

.chart - box:hover {
    transform: scale(1.05);
    box - shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
}

.chart - box h2 {
    font - size: 1.5em;
    margin - bottom: 10px;
}

/* Estilos del formulario */
.form - grid {
    display: grid;
    grid - template - columns: repeat(4, 1fr);
    gap: 15px;
    width: 100 %;
    max - width: 100 %;
}

.form - grid div {
    display: flex;
    flex - direction: column;
}

.form - grid label {
    margin - bottom: 5px;
    font - weight: bold;
    color: #333;
}

.form - grid input, .form - grid select {
    padding: 12px;
    font - size: 1em;
    border: 1px solid #ddd;
    border - radius: 5px;
    transition: border 0.3s ease;
}

.form - grid input: focus, .form - grid select:focus {
    border - color: #1abc9c; /* Borde verde al foco */
    outline: none;
}

.form - grid button {
    padding: 12px 20px;
    background - color: #1abc9c;
    color: white;
    border: none;
    border - radius: 25px;
    cursor: pointer;
    width: 100 %;
    margin - top: 10px;
    font - size: 1.1em;
    transition: background - color 0.3s ease;
}

.form - grid button:hover {
    background - color: #16a085;
}

/* Tabla de Resultados */
.table - container {
    margin - top: 20px;
}

table {
    width: 100 %;
    border - collapse: collapse;
    margin - top: 20px;
    background - color: white;
    border - radius: 10px;
    box - shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

th, td {
    padding: 12px;
    text - align: left;
    border - bottom: 1px solid #ddd;
}

th {
    background - color: #2c3e50;
    color: white;
}

tr:hover {
    background - color: #ecf0f1;
}

/* Footer */
footer p {
    margin: 0;
    font - size: 1.2em;
}

/* Efectos suaves en todo */
* {
    box- sizing: border - box;
margin: 0;
padding: 0;
}

