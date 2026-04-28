# 🧮 Calculator Machine (WPF 기반 고성능 재귀 수식 계산기)

<div align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white">
  <img src="https://img.shields.io/badge/.NET_Framework_4.7.2-512BD4?style=for-the-badge&logo=.net&logoColor=white">
  <img src="https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/MVVM-Binding-blue?style=for-the-badge">
</div>

<br>

## 📌 프로젝트 소개
**Calculator Machine**은 단순한 사칙연산을 넘어, 중첩된 괄호와 복잡한 수식 논리를 정확하게 처리하는 **WPF 기반 데스크톱 계산기**입니다. MVVM 디자인 패턴을 엄격히 준수하여 UI와 비즈니스 로직을 분리하였으며, 특히 데이터 구조의 물리적 조작 대신 **재귀적 구문 분석(Recursive Descent Parsing)** 알고리즘을 도입하여 연산 효율성과 코드 가독성을 극대화한 프로젝트입니다.

## ✨ 핵심 기능 (Key Features)
* **재귀적 수식 분석 엔진:** `ref` 키워드와 공유 인덱스를 활용한 재귀 호출 구조로, 무한히 중첩된 괄호 수식을 메모리 효율적으로 해체 및 계산
* **지능형 수식 정규화(Normalizer):** 단항 음수(`-7`) 처리 및 암묵적 곱셈 기호(`2(3+4)` → `2*(3+4)`) 자동 복원 로직 구현
* **MVVM 아키텍처:** `INotifyPropertyChanged` 알람 시스템과 `RelayCommand`를 통한 완전한 데이터 바인딩 기반 UI 제어
* **실시간 입력 검증(Validator):** 연산자 연속 입력 방지 및 수식 무결성을 실시간으로 감시하여 장비의 논리적 오류를 사전 차단
* **순차적 데이터 파이프라인:** `Tokenizer` → `Normalizer` → `Calculator`로 이어지는 선형적 데이터 처리 구조 설계

<br>

