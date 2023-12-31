using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myfinance_web_netcore.Models;
using myfinance_web_netcore.Services;

namespace myfinance_web_netcore.Controllers;

[Route("[controller]")]
public class TransacaoController : Controller
{
    private readonly ILogger<TransacaoController> _logger;
    private readonly IMapper _mapper;
    private readonly ITransacaoService _transacaoService;
    private readonly IPlanoContaService _planoContaService;

    public TransacaoController(
        ILogger<TransacaoController> logger,
        IMapper mapper,
        ITransacaoService transacaoService,
        IPlanoContaService planoContaService)
    {
        _logger = logger;
        _mapper = mapper;
        _transacaoService = transacaoService;
        _planoContaService = planoContaService;
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
        var transacaoModel = new TransacaoModel();

        if (id != null)
        {
            transacaoModel = _transacaoService.RetornarRegistro((int)id);
        }

        var listaPlanoConta = _planoContaService.ListarPlanoContas();
        var planoContasSelectItems = new SelectList(listaPlanoConta, "Id","Descricao");
        transacaoModel.PlanoContas = planoContasSelectItems ;
        transacaoModel.ListaPlanoConta = listaPlanoConta;
        return View(transacaoModel);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(TransacaoModel model)
    {
        _transacaoService.Salvar(model);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
        _transacaoService.Excluir(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
        var listaTransacao = _transacaoService.ListarTransacoes();
        ViewBag.ListaTransacao = listaTransacao;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
