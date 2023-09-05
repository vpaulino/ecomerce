using ProductsApi.Repository;

namespace ProductsApi.SetupDb
{
    public class InsertProductsService
    {

        Dictionary<string, string> foodProducts = new Dictionary<string, string>
{
    { "Arroz Integral", "Arroz integral de alta qualidade, rico em fibras e nutrientes essenciais. Perfeito para pratos saudáveis." },
    { "Feijão Preto", "Feijão preto de textura macia e saboroso. Ideal para feijoadas e outros pratos brasileiros." },
    { "Macarrão Espaguete", "Macarrão espaguete premium, perfeito para pratos clássicos de massa." },
    { "Molho de Tomate", "Molho de tomate deliciosamente temperado para realçar o sabor das suas receitas italianas." },
    { "Azeite de Oliva Extra Virgem", "Azeite de oliva extra virgem de alta qualidade, perfeito para saladas e cozimento." },
    { "Frango Congelado", "Peitos de frango sem ossos e sem pele, prontos para preparar pratos saudáveis e saborosos." },
    { "Peito de Peru Defumado", "Peito de peru defumado de qualidade premium, ótimo para sanduíches e lanches." },
    { "Pão de Forma Integral", "Pão de forma integral fresco e saudável, perfeito para torradas e sanduíches." },
    { "Leite Integral", "Leite integral fresco e nutritivo, ideal para beber, cozinhar e fazer cereais." },
    { "Iogurte Natural", "Iogurte natural cremoso, rico em probióticos e versátil em várias receitas." },
    { "Queijo Mussarela", "Queijo mussarela fresco e derretível, ótimo para pizzas e lanches." },
    { "Presunto", "Presunto de alta qualidade, ideal para sanduíches e aperitivos." },
    { "Ovos de Galinha", "Ovos de galinha frescos, fonte de proteína de alta qualidade para suas refeições." },
    { "Maçãs", "Maçãs frescas e crocantes, perfeitas para lanches saudáveis e sobremesas." },
    { "Bananas", "Bananas maduras e deliciosas, ricas em potássio e ótimas como lanche rápido." },
    { "Laranjas", "Laranjas suculentas, cheias de vitamina C, ideais para sucos frescos e lanches." },
    { "Cenouras", "Cenouras frescas e crocantes, ideais para lanches saudáveis e acompanhamentos." },
    { "Tomates", "Tomates vermelhos e maduros, ótimos para saladas, molhos e pratos quentes." },
    { "Batatas", "Batatas frescas e versáteis, ideais para fritar, assar ou cozinhar." },
    { "Cebolas", "Cebolas doces e picantes, ótimas para refogados e pratos com sabor." },
    { "Alho", "Alho fresco e aromático, indispensável na culinária para dar sabor a diversos pratos." },
    { "Salmão Fresco", "Salmão fresco de qualidade premium, perfeito para grelhar e assar." },
    { "Atum em Lata", "Atum em lata de alta qualidade, ótimo para saladas e sanduíches saudáveis." },
    { "Abacaxi", "Abacaxi maduro e doce, ideal para sobremesas tropicais e sucos frescos." },
    { "Morangos", "Morangos vermelhos e suculentos, ótimos para sobremesas e lanches." },
    { "Uvas", "Uvas frescas e doces, perfeitas para lanches rápidos e saladas de frutas." },
    { "Abacate", "Abacates cremosos e saudáveis, ideais para guacamole e smoothies." },
    { "Cereais Matinais", "Cereais matinais crocantes e nutritivos, ideais para um café da manhã saudável." },
    { "Açúcar", "Açúcar refinado de alta qualidade, perfeito para adoçar bebidas e sobremesas." },
    { "Farinha de Trigo", "Farinha de trigo versátil, ideal para fazer pães, bolos e massas caseiras." },
    { "Café em Grãos", "Café em grãos frescos e aromáticos, perfeito para moer e preparar café fresco." },
    { "Chá Verde", "Chá verde refrescante, carregado de antioxidantes e benefícios para a saúde." },
    { "Bolachas de Chocolate", "Bolachas de chocolate deliciosas, perfeitas para um lanche doce." },
    { "Geléia de Morango", "Geléia de morango caseira, ótima para passar no pão e bolos." },
    { "Mel", "Mel puro e natural, ideal para adoçar chás, iogurtes e panquecas." },
    { "Manteiga", "Manteiga cremosa e saborosa, perfeita para passar no pão e cozinhar." },
    { "Refrigerante de Cola", "Refrigerante de cola refrescante, ótimo para acompanhar pizzas e hambúrgueres." },
    { "Suco de Laranja", "Suco de laranja fresco e rico em vitamina C, ideal para o café da manhã." },
    { "Cerveja", "Cerveja gelada, perfeita para momentos de descontração com os amigos." },
    { "Vinho Tinto", "Vinho tinto encorpado e saboroso, ótimo para apreciar com pratos de carne." },
    { "Água Mineral", "Água mineral pura e refrescante, essencial para se manter hidratado." },
    { "Pipoca de Micro-ondas", "Pipoca de micro-ondas prática e saborosa, perfeita para assistir a filmes em casa." },
    { "Pizza Congelada", "Pizza congelada de alta qualidade, com variedade de sabores para satisfazer seu paladar." },
    { "Ketchup", "Ketchup delicioso, ideal para acompanhar hambúrgueres e batatas fritas." },
    { "Mostarda", "Mostarda suave e saborosa, perfeita para sanduíches e cachorros-quentes." },
    { "Molho de Soja", "Molho de soja autêntico, ideal para dar um toque oriental aos seus pratos." },
    { "Pepinos", "Pepinos frescos e crocantes, ótimos para saladas e conservas." },
    { "Queijo Cheddar", "Queijo cheddar envelhecido, perfeito para nachos, hambúrgueres e tacos." },
    { "Queijo Parmesão", "Queijo parmesão ralado de alta qualidade, ideal para polvilhar massas e saladas." },
    { "Salsichas", "Salsichas saborosas, ótimas para grelhar, assar ou cozinhar com molho." },
    { "Salsichas Vegetarianas", "Salsichas vegetarianas saborosas, uma opção saudável e deliciosa." },
    { "Molho de Pimenta", "Molho de pimenta picante, ideal para dar um toque ardente aos seus pratos." },
    { "Picles", "Picles em conserva, ótimos para acompanhar sanduíches e hambúrgueres." },
    { "Creme de Leite", "Creme de leite cremoso e versátil, perfeito para molhos e sobremesas." },
    { "Requeijão", "Requeijão cremoso e saboroso, ideal para passar no pão e biscoitos." },
    { "Cereais de Aveia", "Cereais de aveia crocantes e saudáveis, ricos em fibras e nutrientes." },
    { "Barras de Granola", "Barras de granola energéticas, ideais como lanches rápidos e nutritivos." },
    { "Gelado de Baunilha", "Sorvete de baunilha cremoso, perfeito para sobremesas e acompanhamentos." },
    { "Chocolate Amargo", "Chocolate amargo de alta qualidade, para os amantes de chocolate com sabor intenso." },
    { "Coco Ralado", "Coco ralado fresco e versátil, ideal para sobremesas e bolos tropicais." },
    { "Biscoitos de Aveia", "Biscoitos de aveia caseiros, perfeitos para lanches saudáveis." },
    { "Geleia de Framboesa", "Geleia de framboesa doce e frutada, ótima para passar no pão e bolos." },
    { "Ameixas Secas", "Ameixas secas suculentas, perfeitas como lanches naturais e saudáveis." },
    { "Passas", "Passas doces e nutritivas, ótimas para cereais e saladas de frutas." },
    { "Melancia", "Melancia doce e refrescante, perfeita para dias quentes de verão." },
    { "Melão", "Melão maduro e suculento, ótimo para sobremesas e saladas de frutas." },
    { "Mamão", "Mamão maduro e doce, ideal para vitaminas e sobremesas tropicais." },
    { "Melão Cantalupo", "Melão cantalupo de aroma doce, ótimo para lanches e sobremesas." },
    { "Amendoins", "Amendoins crocantes e saborosos, ótimos como petiscos e aperitivos." },
    { "Nozes", "Nozes frescas e saudáveis, ricas em ômega-3 e ideais para snacks." },
    { "Amêndoas", "Amêndoas inteiras e nutritivas, perfeitas para lanches e cereais." },
    { "Castanhas de Caju", "Castanhas de caju torradas e salgadas, um snack saudável e saboroso." },
    { "Iogurte de Morango", "Iogurte de morango cremoso e frutado, rico em probióticos e sabor." },
    { "Pão de Centeio", "Pão de centeio fresco e saudável, perfeito para sanduíches e torradas." },
    { "Aipo", "Aipo fresco e crocante, ideal para snacks saudáveis e saladas." },
    { "Ervilhas", "Ervilhas verdes e suculentas, ótimas para acompanhamentos e sopas." },
    { "Espinafre", "Espinafre fresco e nutritivo, perfeito para saladas e pratos quentes." },
    { "Brócolis", "Brócolis verde e saudável, rico em nutrientes e versátil em várias receitas." },
    { "Queijo Feta", "Queijo feta cremoso e salgado, ideal para saladas gregas e pratos mediterrâneos." },
    { "Creme de Amendoim", "Creme de amendoim cremoso e rico em proteínas, ótimo para passar no pão e smoothies." },
    { "Sorvete de Chocolate", "Sorvete de chocolate indulgente, perfeito para sobremesas e momentos de prazer." },
    { "Pão de Alho", "Pão de alho macio e saboroso, ótimo para churrascos e acompanhamentos." },
    { "Pão de Cebola", "Pão de cebola fresco e aromático, ideal para sanduíches e torradas." },
    { "Creme de Espinafre", "Creme de espinafre cremoso e reconfortante, ótimo como acompanhamento." },
    { "Sopa de Tomate enlatada", "Sopa de tomate enlatada, prática e deliciosa para aquecer em dias frios." },
    { "Pimentões", "Pimentões vermelhos e amarelos, ótimos para saladas, grelhados e molhos." },
    { "Couve-Flor", "Couve-flor fresca e versátil, perfeita para gratinados e pratos saudáveis." },
    { "Arroz Basmati", "Arroz basmati aromático e leve, ideal para acompanhar pratos indianos e orientais." },
    { "Feijão Vermelho", "Feijão vermelho de textura suave, ótimo para chili e pratos mexicanos." },
    { "Manteiga de Amendoim Crunchy", "Manteiga de amendoim com pedaços crocantes, perfeita para sanduíches e snacks." },
    { "Aveia Instantânea", "Aveia instantânea rápida e saudável, perfeita para café da manhãs agitados." },
    { "Salmão Defumado", "Salmão defumado de alta qualidade, perfeito para aperitivos e canapés." },
    { "Molho de Churrasco", "Molho de churrasco saboroso, ideal para grelhados e churrascos." },
    { "Feijão Verde", "Feijão verde fresco e crocante, ótimo para acompanhamentos e pratos saudáveis." },
    { "Alface", "Alface fresca e crocante, perfeita para saladas e sanduíches." },
    { "Mel de Maple", "Mel de maple puro e natural, ideal para panquecas e waffles." },
    { "Biscoitos de Aveia com Passas", "Biscoitos de aveia com passas macios e recheados de sabor, ótimos para lanches." },
    { "Salsichas de Peru", "Salsichas de peru saudáveis e saborosas, ideais para grelhar e cozinhar." },
    { "Iogurte Grego com Mel", "Iogurte grego cremoso com mel, uma combinação perfeita de doçura e cremosidade." },
    { "Cereais de Trigo", "Cereais de trigo crocantes e nutritivos, ideais para um café da manhã saudável." },
    { "Chocolates Recheados", "Chocolates recheados com sabor delicioso, perfeitos para indulgências." },
    { "Creme de Frango enlatado", "Creme de frango enlatado, ótimo para sopas cremosas e receitas rápidas." },
    { "Creme de Cogumelos enlatado", "Creme de cogumelos enlatado, ideal para molhos e pratos de frango." },
    { "Maionese", "Maionese cremosa e versátil, perfeita para sanduíches e saladas." },
    { "Massa Penne", "Massa penne de trigo durum, ótima para pratos de massa com molhos diversos." },
    { "Maçãs Fuji", "Maçãs Fuji frescas e doces, perfeitas para lanches e sobremesas." },
    { "Bananas Orgânicas", "Bananas orgânicas frescas e saudáveis, ideais para lanches e smoothies." },
    { "Lasanha de Carne", "Lasanha de carne saborosa, perfeita para assar no forno e servir em família." },
    { "Pizza de Pepperoni", "Pizza de pepperoni com queijo derretido, pronta para assar e saborear." },
    { "Frango Assado Congelado", "Frango assado congelado, suculento e temperado, ideal para refeições rápidas." },
    { "Batatas Assadas Congeladas", "Batatas assadas congeladas, crocantes por fora e macias por dentro." },
    { "Empadão de Frango", "Empadão de frango congelado, uma refeição completa e reconfortante." },
    { "Massa Folhada", "Massa folhada congelada, versátil para preparar salgados e doces." },
    { "Torta de Maçã Congelada", "Torta de maçã congelada, uma sobremesa deliciosa para assar e servir." },
    { "Peixe Empanado", "Peixe empanado congelado, crocante por fora e suculento por dentro." },
    { "Nuggets de Frango", "Nuggets de frango congelados, perfeitos como petiscos ou acompanhamentos." },
    { "Pão de Alho Congelado", "Pão de alho congelado, ideal para churrascos e refeições ao ar livre." },
    { "Quiche de Espinafre", "Quiche de espinafre congelada, uma opção saborosa para lanches ou entradas." },
    { "Torta de Carne", "Torta de carne congelada, recheada e pronta para assar e servir." }
    // Adicione mais produtos e descrições conforme necessário
};


        private readonly SqlServerProductsRepository repository;

        public InsertProductsService(SqlServerProductsRepository repository)
        {
            this.repository = repository;
        }


        public async Task ExecuteAsync(CancellationToken token) 
        {
            foreach (var item in foodProducts)
            {
                await this.repository.AddProductAsync(new Product(item.Key, item.Value, "Food", 3), token);
            }
        }
    }
}
