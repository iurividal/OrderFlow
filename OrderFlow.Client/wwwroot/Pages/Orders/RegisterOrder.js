window.imprimirPedido = function (pedido, itens) {
    var printWindow = window.open('', '_blank');

    console.log(pedido);
    console.log(itens);

    var html = `
        <html>
        <head>
            <title>Pedido ${pedido.number}</title>
            <style>
                body { font-family: Arial, sans-serif; margin: 20px; }
                h1 { text-align: center; }
                table { width: 100%; border-collapse: collapse; }
                th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                th { background-color: #f2f2f2; }
                .total { text-align: right; font-weight: bold; margin-top: 20px; }
                .footer { margin-top: 50px; text-align: center; font-size: 12px; }
            </style>
        </head>
        <body>
            <h1>Pedido #${pedido.number}</h1>
            
            <div class="info">
                <p><strong>Data:</strong> ${new Date(pedido.Data).toLocaleDateString()}</p>
                <p><strong>Cliente:</strong> ${pedido.customer.name}</p>
                <p><strong>Endereço:</strong> ${pedido.customer.address[0].street} ${pedido.customer.address[0].number} - ${pedido.customer.address[0].city}</p>
                <p><strong>Telefone:</strong> ${pedido.customer.phone}</p>
            </div>
            
            <table>
                <thead>
                    <tr>
                        <th>Código</th>
                        <th>Produto</th>
                        <th>Preço Unit.</th>
                        <th>Qtd</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
    `;

    itens.forEach(item => {
        html += `
            <tr>
                <td>${item.code}</td>
                <td>${item.name}</td>
                <td>R$ ${item.price.toFixed(2).replace('.', ',')}</td>
                <td>${item.Quantity}</td>
                <td>R$ ${item.subtotal.toFixed(2).replace('.', ',')}</td>
            </tr>
        `;
    });

    const total = itens.reduce((sum, item) => sum + item.subtotal, 0);

    console.log("Total:" + total);

    html += `
                </tbody>
            </table>
            
            <div class="total">
                <p>Total do Pedido: R$ ${total.toFixed(2).replace('.', ',')}</p>
            </div>
            
            <div class="footer">
                <p>Este documento não possui valor fiscal.</p>
            </div>
        </body>
        </html>
    `;

    printWindow.document.write(html);
    printWindow.document.close();

    // Aguardar o carregamento do conteúdo e imprimir
    printWindow.onload = function () {
        printWindow.print();
    }
};