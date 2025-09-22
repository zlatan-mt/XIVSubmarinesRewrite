// apps/XIVSubmarinesRewrite/Properties/AssemblyInfo.cs
// 内部メンバーをテストプロジェクトから参照できるようにします
// テスト容易性を高めるため、内部 API のアクセス範囲を制御する目的で存在します
// RELEVANT FILES: apps/XIVSubmarinesRewrite/tests/XIVSubmarinesRewrite.Tests/XIVSubmarinesRewrite.Tests.csproj

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("XIVSubmarinesRewrite.Tests")]
